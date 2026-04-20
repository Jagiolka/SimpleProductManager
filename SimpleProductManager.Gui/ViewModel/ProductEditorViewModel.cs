using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleProductManager.Gui.Manager;
using SimpleProductServices.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleProductManager.Gui.ViewModel;

using ILogger = Serilog.ILogger;

public partial class ProductEditorViewModel(ILogger logger, IHttpClientManager httpClientManager) : ObservableObject
{
    [ObservableProperty] 
    private SimpleProductModel? editingSimpleProductModel; 
    
    // ComboBoxProductCategories - ItemsSource
    [ObservableProperty] 
    private ObservableCollection<SimpleProductCategoryModel>? productCategories = [];
    
    // ComboBoxProductCategories - SelectedItem
    [ObservableProperty]
    private SimpleProductCategoryModel? selectedProductCategory;
    
    [ObservableProperty]
    private string selectedStringName = string.Empty;
    
    [ObservableProperty]
    private string errorMessage = string.Empty;

    /// <summary>
    /// initializes the product editor with a product model
    /// </summary>
    /// <param name="productModel">Product model to be edited. Will be created a new if NULL.</param>
    public async Task InitProductEditorAsync(SimpleProductModel productModel)
    {
        this.ErrorMessage = string.Empty;
        
        await RefreshProductCategoryAsync();

        EditingSimpleProductModel = new SimpleProductModel(
            productModel.Id,
            productModel.Name,
            productModel.Description,
            productModel.Price,
            productModel.SimpleProductCategory);
    }
    
    [RelayCommand]
    private async Task AddCategoryAsync(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            LogError("Name der Kategorie muss angegeben werden.");
            return;
        }

        if (ProductCategories == null)
        {
            LogError("Kategorien sind NULL.");
            return;
        }
        
        if (ProductCategories!.Any(spc => spc.Name == categoryName))
        {
            LogError("Name der Kategorie existiert bereits.");
            return;
        }
        
        var (errorMsg, result) = await httpClientManager.AddNewSimpleProductCategoryAsync(categoryName);

        if (!string.IsNullOrEmpty(errorMsg))
        {
            LogError(errorMsg);
        }
        
        if (result != null)
        {
            await RefreshProductCategoryAsync();
            SelectedProductCategory = ProductCategories!.First(pc => pc.Name == categoryName);
        }
    }
    
    [RelayCommand]
    private async Task RemoveCategoryAsync()
    {
        if (SelectedProductCategory == null)
        {
            return;
        }
    
        await httpClientManager.RemoveSimpleProductCategoryAsync(SelectedProductCategory.Id);
        await RefreshProductCategoryAsync();
    }
    
    [RelayCommand]
    private async Task SaveExitAsync(Window window)
    {
        if (EditingSimpleProductModel.Id == Guid.Empty)
        {
            LogError("Id ist erforderlich");
            return;
        }
        
        if (string.IsNullOrWhiteSpace(EditingSimpleProductModel.Name))
        {
            LogError("Name ist erforderlich");
            return;
        }

        if (EditingSimpleProductModel.Price < 0)
        {
            LogError("Preis darf nicht negativ sein");
            return;
        }
        
        try
        {
            await Task.CompletedTask;
            window.DialogResult = true;
            window.Close();
        }
        catch (Exception ex)
        {
            LogError("Fehler beim Speichern: " + ex.Message);
        }
    }

    [RelayCommand]
    private void CancelExit(Window window)
    {
        window.DialogResult = false;
        window.Close();
    }
    
    /// <summary>
    /// Load all product categories from the API.
    /// </summary>
    private async Task<List<SimpleProductCategoryModel>> LoadProductCategoriesAsync()
    {
        var (message, categories) = await httpClientManager.GetAllSimpleProductCategoriesAsync();
    
        if (!string.IsNullOrEmpty(message))
        {
            LogError($"Fehler beim Laden der Kategorien: {message}");
            
            return [];
        }
    
        return categories;
    }

    private async Task RefreshProductCategoryAsync()
    {
        var productCategoryList = await LoadProductCategoriesAsync();
        this.ProductCategories = new ObservableCollection<SimpleProductCategoryModel>(productCategoryList);

        // set SelectedItem
        if (this.ProductCategories.Any())
        {
            SelectedProductCategory = ProductCategories[0];
        }
    }

    private void LogError(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }
        
        ErrorMessage = string.Empty;
        ErrorMessage = $"[{DateTime.Now.ToLongTimeString()}] - {message}";
        logger.Error(ErrorMessage);
    }
}