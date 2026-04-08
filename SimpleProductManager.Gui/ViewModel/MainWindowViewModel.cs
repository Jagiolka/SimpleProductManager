using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SimpleProductManager.Gui.Manager;
using SimpleProductManager.Gui.View;
using SimpleProductServices.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WpfArchiver.Ressources;

using ILogger = Serilog.ILogger;

namespace SimpleProductManager.Gui.ViewModel;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ILogger logger;
    private readonly IServiceProvider serviceProvider;
    private readonly IHttpClientManager httpClientManager;
    private ProductEditorWindow dialogWindow;
    
    [ObservableProperty]
    private string errorMessage = string.Empty;
    
    [ObservableProperty]
    private ObservableCollection<SimpleProductCategoryModel?> productCategories = [];

    [ObservableProperty]
    private ObservableCollection<SimpleProductModel> filteredSimpleProductList  = []; // SimpleProductModelList filtered by simpleProductListFilterText

    private ObservableCollection<SimpleProductModel> simpleProductModelList  = [];    // unfiltered list of SimpleProductModel
    public ObservableCollection<SimpleProductModel> SimpleProductModelList
    {
        get => this.simpleProductModelList;
        set
        {
            SetProperty(ref this.simpleProductModelList, value);
            SetFilteredSimpleProductList(this.simpleProductListFilterText);
        }
    }

    private string simpleProductListFilterText;
    public string SimpleProductListFilterText
    {
        get => this.simpleProductListFilterText;
        set 
        {
            SetProperty(ref this.simpleProductListFilterText, value);
            SetFilteredSimpleProductList(this.simpleProductListFilterText);
        }
    }

    public MainWindowViewModel(ILogger logger, IServiceProvider serviceProvider, IHttpClientManager httpClientManager)
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
        this.httpClientManager = httpClientManager;
        
        
        this.dialogWindow = this.serviceProvider.GetRequiredService<ProductEditorWindow>();

        Task.Run(LoadAllSimpleProductsAsync);
        var productCategoriesList = Task.Run(GetProductCategoriesAsync).Result;
        this.ProductCategories = new ObservableCollection<SimpleProductCategoryModel?>(productCategoriesList!);
    }

    [RelayCommand]
    private async Task LoadProductListAsync()
    {
        await LoadAllSimpleProductsAsync();
    }

    [RelayCommand]
    private async Task AddSimpleProductAsync()
    {
        dialogWindow = this.serviceProvider.GetRequiredService<ProductEditorWindow>();
        try
        {
            // var spc = new SimpleProductCategoryModel(new Guid(), "TEST");
            var newGuid = Guid.NewGuid();
            var newProductModel = new SimpleProductModel(newGuid, string.Empty, string.Empty, 0, null);
            var productViewModelDataContext = (ProductEditorViewModel)dialogWindow.DataContext;                
            await productViewModelDataContext.InitProductEditorAsync(newProductModel);

            if (dialogWindow.ShowDialog() == true)
            {
                await this.httpClientManager.AddNewSimpleProductAsync(newProductModel);
                // this.logger.Information(string.Format(ConstantMessages.MainWindowViewModel_AddProduct, productViewModelDataContext.EditingSimpleProductModel.Id));
                await this.LoadAllSimpleProductsAsync();
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
        }       
    }
    
    [RelayCommand]
    private async Task EditSimpleProductAsync(SimpleProductModel? editingSimpleProduct)
    {
        if (editingSimpleProduct is null)
        {
            return;
        }

        dialogWindow = this.serviceProvider.GetRequiredService<ProductEditorWindow>();

        var productViewModelDataContext = (ProductEditorViewModel)dialogWindow.DataContext;
        productViewModelDataContext.InitProductEditorAsync(editingSimpleProduct);

        var dialogResult = dialogWindow.ShowDialog() ?? false;

        if (dialogResult)
        {            
            var result = await this.httpClientManager.UpdateSimpleProductAsync(editingSimpleProduct);
            // ShowTemporaryErrorMessage(errorMessage);
            this.logger.Information(string.Format(ConstantMessages.MainWindowViewModel_EditProduct, editingSimpleProduct.Id));
        }
    }

    [RelayCommand]
    private async Task RemoveSimpleProductAsync(SimpleProductModel? removingSimpleProduct)
    {
        if (removingSimpleProduct is null)
        { 
            return; 
        }

        Guid removingSimpleProductId = removingSimpleProduct.Id;
        await this.httpClientManager.RemoveSimpleProductAsync(removingSimpleProduct.Id);
        await this.LoadProductListAsync();
        this.logger.Information(string.Format(ConstantMessages.MainWindowViewModel_RemoveProduct, removingSimpleProductId));
    }
    
    private async Task LoadAllSimpleProductsAsync()
    {
        var response = await this.httpClientManager.GetAllSimpleProductAsync();
        ShowTemporaryErrorMessage(response.Item1);
        this.SimpleProductModelList = new ObservableCollection<SimpleProductModel>(response.Item2);

        this.logger.Information(string.Format(ConstantMessages.MainWindowViewModel_LoadProducts, this.FilteredSimpleProductList.Count));
    }
    
    private void SetFilteredSimpleProductList(string filterText)
    {
        this.FilteredSimpleProductList = 
            string.IsNullOrWhiteSpace(filterText)
            ? this.SimpleProductModelList
            : new ObservableCollection<SimpleProductModel>(
                this.SimpleProductModelList.Where(spmList => spmList.Name.Contains(filterText))
                .ToList());
    }

    private async Task<List<SimpleProductCategoryModel>> GetProductCategoriesAsync()
    {
        var response = await this.httpClientManager.GetAllSimpleProductCategoriesAsync();
        ShowTemporaryErrorMessage(response.Item1);
        var simpleProductCategoryModelList = response.Item2;
        
        return simpleProductCategoryModelList;
    }

    private void ShowTemporaryErrorMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }
        
        this.ErrorMessage = $"[{DateTime.Now.ToShortTimeString()}] - {message}";
        logger.Error(this.ErrorMessage);
    }
}
