using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleProductManager.Gui.Manager;
using SimpleProductServices.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleProductManager.Gui.ViewModel;

using ILogger = Serilog.ILogger;

public partial class ProductEditorViewModel(ILogger logger, IHttpClientManager httpClientManager) : ObservableObject
{
    [ObservableProperty] 
    private SimpleProductModel editingSimpleProductModel; 
    // new SimpleProductModel(new Guid(), string.Empty, string.Empty, 0, new SimpleProductCategoryModel(new Guid(), string.Empty));

    
    // ComboBoxProductCategories - ItemsSource
    [ObservableProperty] 
    private ObservableCollection<SimpleProductCategoryModel>? productCategories = [];
    
    // ComboBoxProductCategories - SelectedItem
    [ObservableProperty]
    private SimpleProductCategoryModel? selectedProductCategory;
    
    [ObservableProperty]
    private string selectedStringName = string.Empty;
    
    
    [ObservableProperty]
    private Dictionary<string, string> fieldErrors = new();

    [ObservableProperty]
    private bool hasErrors;
    

    /// <summary>
    /// initializes the product editor with a product model
    /// </summary>
    /// <param name="productModel">Product model to be edited. Will be created a new if NULL.</param>
    public async Task InitProductEditorAsync(SimpleProductModel productModel)
    {
        await RefreshProductCategoryAsync();

        EditingSimpleProductModel = new SimpleProductModel(
            productModel.Id,
            productModel.Name,
            productModel.Description,
            productModel.Price,
            productModel.SimpleProductCategory);
        
        ClearErrors();
    }
    
    [RelayCommand]
    private async Task AddCategoryAsync(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            return;
        }

        if (ProductCategories != null && 
            ProductCategories.Any(spc => spc.Name == categoryName))
        {
            return;
        }
        
        var (errorMessage, result) = await httpClientManager.AddNewSimpleProductCategoryAsync(categoryName);
        
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
            AddFieldError(nameof(SimpleProductModel.Id), "Id ist erforderlich");
            return;
        }
        
        if (string.IsNullOrWhiteSpace(EditingSimpleProductModel.Name))
        {
            AddFieldError(nameof(SimpleProductModel.Name), "Name ist erforderlich");
            return;
        }

        if (EditingSimpleProductModel.Price < 0)
        {
            AddFieldError(nameof(SimpleProductModel.Price), "Preis darf nicht negativ sein");
            return;
        }
        

        try
        {
            // Hier könnte die API-Anfrage erfolgen
            await Task.CompletedTask;
            window.DialogResult = true;
            window.Close();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Fehler beim Speichern");
            AddFieldError("general", "Fehler beim Speichern: " + ex.Message);
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
        var (errorMessage, categories) = await httpClientManager.GetAllSimpleProductCategoriesAsync();
    
        if (!string.IsNullOrEmpty(errorMessage))
        {
            FieldErrors.Add("Fehler beim Laden der Kategorien", $"Fehler beim Laden der Kategorien: {errorMessage}");
            
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

    private void AddFieldError(string fieldName, string errorMessage)
    {
        FieldErrors[fieldName] = errorMessage;
        HasErrors = FieldErrors.Count > 0;
        OnPropertyChanged(nameof(FieldErrors));
    }

    private void ClearErrors()
    {
        FieldErrors.Clear();
        HasErrors = false;
        OnPropertyChanged(nameof(FieldErrors));
    }
}