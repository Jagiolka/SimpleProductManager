namespace SimpleProductManager.Gui.ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleProductManager.DataLayer;
using SimpleProductManager.DataLayer.DataModel;
using SimpleProductManager.Gui.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WpfArchiver.Ressources;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ILogger<MainWindowViewModel> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly IHttpClientManager httpClientManager;
    private readonly ProductEditorViewModel productEditorViewModel;

    [ObservableProperty]
    private List<ProductCategoryModel> productCategories = new List<ProductCategoryModel>();


    private string productListFilterText;
    public string ProductListFilterText
    {
        get => this.productListFilterText;
        set 
        {
            SetProperty(ref this.productListFilterText, value);
            SetFilteredSimpleProductList(this.productListFilterText);
        }
    }

    private ObservableCollection<SimpleProductStockModel> filteredSimpleProductList;
    public ObservableCollection<SimpleProductStockModel> FilteredSimpleProductList 
    {
        get => this.filteredSimpleProductList;
        set => SetProperty(ref this.filteredSimpleProductList, value); 
    }

    private ObservableCollection<SimpleProductStockModel> simpleProductStockList = new();
    public ObservableCollection<SimpleProductStockModel> SimpleProductStockList
    {
        get => this.simpleProductStockList;
        set
        {
            SetProperty(ref this.simpleProductStockList, value);
            SetFilteredSimpleProductList(this.productListFilterText);
        }
    }

    private SimpleProductStockModel selectedProductStock;
    public SimpleProductStockModel SelectedProductStock
    {
        get => this.selectedProductStock;
        set => SetProperty(ref this.selectedProductStock, value);
    }    

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, IServiceProvider serviceProvider, IHttpClientManager httpClientManager, ProductEditorViewModel productEditorViewModel)
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
        this.httpClientManager = httpClientManager;
        this.productEditorViewModel = productEditorViewModel;
        this.FilteredSimpleProductList = [];

        this.dialogWindow = this.serviceProvider.GetRequiredService<ProductEditorWindow>();
        this.ProductCategories = Task.Run(GetProductCategoriesAsync).Result; 
    }

    [RelayCommand]
    public async Task LoadProductStockListAsync()
    {
        var loadedProductStockList = await this.httpClientManager.GetSimpleProductStockAsync();
        this.SimpleProductStockList = new ObservableCollection<SimpleProductStockModel>(loadedProductStockList);

        this.logger.LogInformation(string.Format(ConstantMessages.MainWindowViewModel_LoadProducts, this.FilteredSimpleProductList.Count));
    }

    private ProductEditorWindow dialogWindow;

    [RelayCommand]
    public async Task AddProductStockAsync()
    {
        dialogWindow = this.serviceProvider.GetRequiredService<ProductEditorWindow>();

        var productViewModelDataContext = (ProductEditorViewModel)dialogWindow.DataContext;
        productViewModelDataContext.Init(null, ProductCategories);

        

        var dialogResult = dialogWindow.ShowDialog() ?? false;

        if (dialogResult)
        {
            await this.AddSimpleProductAsync(productViewModelDataContext.EditingSimpleProductStockModel);
            this.logger.LogInformation(string.Format(ConstantMessages.MainWindowViewModel_AddProduct, productViewModelDataContext.EditingSimpleProductStockModel.SimpleProductModelId));
        }
    }

    [RelayCommand]
    public async Task EditProductStockAsync()
    {        
        dialogWindow = this.serviceProvider.GetRequiredService<ProductEditorWindow>();

        var productViewModelDataContext = (ProductEditorViewModel)dialogWindow.DataContext;
        productViewModelDataContext.Init(this.SelectedProductStock, ProductCategories);

        // productViewModelDataContext.SimpleProductStockModel = this.SelectedProductStock;
        var dialogResult = dialogWindow.ShowDialog() ?? false;

        if (dialogResult)
        {
            await UpdateSimpleProductAsync(this.SelectedProductStock);
            this.logger.LogInformation(string.Format(ConstantMessages.MainWindowViewModel_AddProduct, this.SelectedProductStock.SimpleProductModelId));
        }
    }

    [RelayCommand]
    public async Task RemoveProductStockAsync()
    {
        await this.RemoveSimpleProductAsync(this.SelectedProductStock);
        this.logger.LogInformation(string.Format(ConstantMessages.MainWindowViewModel_RemoveProduct, this.SelectedProductStock.SimpleProductModelId));
    }
    
    private void SetFilteredSimpleProductList(string filterText)
    {
        this.FilteredSimpleProductList = 
            string.IsNullOrWhiteSpace(filterText)
            ? this.SimpleProductStockList
            : new ObservableCollection<SimpleProductStockModel>(
                this.SimpleProductStockList.Where(simpleProductStockList => simpleProductStockList.Name.Contains(filterText))
                .ToList());
    }

    private async Task<List<ProductCategoryModel>> GetProductCategoriesAsync() 
    {
        return await this.httpClientManager.GetProductCategoriesAsync();
    }

    private async Task AddSimpleProductAsync(SimpleProductStockModel simpleProductStockModel)
    {
        this.SimpleProductStockList.Add(simpleProductStockModel);
        await this.httpClientManager.AddSimpleProductAsync(simpleProductStockModel);
    }

    private async Task RemoveSimpleProductAsync(SimpleProductStockModel simpleProductStockModel)
    {
        this.SimpleProductStockList.Remove(simpleProductStockModel);
        await this.httpClientManager.RemoveProductStockAsync(simpleProductStockModel);
    }

    private async Task UpdateSimpleProductAsync(SimpleProductStockModel simpleProductStockModel) 
    {
        // TODO: update
        //await this.httpClientManager.UpdateSimpleProductAsync(simpleProductStockModel);
    }
}
