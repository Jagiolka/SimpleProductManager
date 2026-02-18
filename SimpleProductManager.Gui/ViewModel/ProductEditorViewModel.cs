using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleProductManager.Gui.Manager;
using SimpleProductServices.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ILogger = Serilog.ILogger;

namespace SimpleProductManager.Gui.ViewModel;

public partial class ProductEditorViewModel(ILogger logger, IHttpClientManager httpClientManager) : ObservableObject
{
    [ObservableProperty]
    private string selectedStringName;

    [ObservableProperty]
    private SimpleProductModel editingSimpleProductModel;

    [ObservableProperty]
    private ObservableCollection<SimpleProductCategoryModel?> productCategories;

    [ObservableProperty]
    private SimpleProductCategoryModel? selectedComboBoxProductCategory;

    public void Init(SimpleProductModel? productModel, ObservableCollection<SimpleProductCategoryModel?> productCategories)
    {
        productModel ??= new SimpleProductModel(Guid.NewGuid(), string.Empty, string.Empty, 0, null);

        this.EditingSimpleProductModel = productModel;
        this.ProductCategories = productCategories;

        if (this.ProductCategories.Any()) 
        {
            this.SelectedComboBoxProductCategory = ProductCategories.First();
        }
    }
    
    [RelayCommand]
    private async Task AddCategoryAsync(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName) 
            || ProductCategories.Any(pc => pc.Name == categoryName))
        {
            return;
        }

        var response =  await httpClientManager.AddNewSimpleProductCategoryAsync(categoryName);
        
        // string errorMessage = response.Item1;
        ProductCategories.Add(response.Item2);
        SelectedComboBoxProductCategory = response.Item2;
    }

    [RelayCommand]
    private async Task RemoveCategoryAsync()
    {
        if (this.SelectedComboBoxProductCategory is null)
        {
            return;
        }

        await httpClientManager.RemoveSimpleProductCategoryAsync(this.SelectedComboBoxProductCategory.Id);
        this.ProductCategories.Remove(this.SelectedComboBoxProductCategory);
    }

    [RelayCommand]
    private async Task SaveExitAsync(Window window)
    {
        this.EditingSimpleProductModel.SimpleProductCategory = SelectedComboBoxProductCategory;
        if (this.IsEveryPropertyValid())
        {
            window.DialogResult = true;
            CloseWindow(window);
        }
    }

    [RelayCommand]
    private async Task CancelExitAsync(Window window)
    {
        window.DialogResult = false;
        CloseWindow(window);
    }

    private bool IsEveryPropertyValid()
    {
        return !EditingSimpleProductModel.Id.Equals(Guid.Empty) ||
            !string.IsNullOrWhiteSpace(EditingSimpleProductModel.Name) ||
            !string.IsNullOrWhiteSpace(EditingSimpleProductModel.Description) ||
            EditingSimpleProductModel.SimpleProductCategory is not null;
    }
    
    private static void CloseWindow(Window window)
    {
        window?.Close();
    }       
}