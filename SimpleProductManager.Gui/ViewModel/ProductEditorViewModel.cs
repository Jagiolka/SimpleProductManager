namespace SimpleProductManager.Gui.ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SimpleProductManager.DataLayer.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

public partial class ProductEditorViewModel : ObservableObject
{
    private readonly ILogger logger;

    [ObservableProperty]
    private SimpleProductStockModel editingSimpleProductStockModel = new SimpleProductStockModel();

    [ObservableProperty]
    private List<ProductCategoryModel> productCategories = [];

    [ObservableProperty]
    private ProductCategoryModel selectedProductStockProductCategory;

    [ObservableProperty]
    private ProductCategoryModel selectedComboBoxProductCategory;

    public ProductEditorViewModel(ILogger<ProductEditorViewModel> logger)
    {
        this.logger = logger;
    }

    public void Init(SimpleProductStockModel simpleProductStockModel, List<ProductCategoryModel> productCategories)
    {
        this.EditingSimpleProductStockModel = simpleProductStockModel ?? new SimpleProductStockModel();

        this.ProductCategories = productCategories;
    }

    [RelayCommand]
    public async Task AddCategoryAsync()
    {
        if (this.SelectedComboBoxProductCategory is null)
        {
            return;
        }

        this.EditingSimpleProductStockModel.ProductCategories.Add(this.SelectedComboBoxProductCategory);
    }

    [RelayCommand]
    public async Task RemoveCategoryAsync()
    {
        if (this.SelectedProductStockProductCategory is null)
        {
            return;
        }

        this.EditingSimpleProductStockModel.ProductCategories.Remove(this.SelectedProductStockProductCategory);
    }

    [RelayCommand]
    public async Task SaveExitAsync(Window window)
    {
        var spm = this.EditingSimpleProductStockModel;
        if (this.IsEveryPropertyValid())
        {
            window.DialogResult = true;
            this.CloseWindow(window);
        }
    }

    private bool IsEveryPropertyValid()
    {

        return true;
    }

    [RelayCommand]
    public async Task CancelExitAsync(Window window)
    {
        window.DialogResult = false;
        this.CloseWindow(window);
    }

    private void CloseWindow(Window window)
    {
        if (window != null)
        {
            window.Close();
        }
    }
}
