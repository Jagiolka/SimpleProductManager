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
using System.Windows.Threading;
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
    private string errorMessage;
    private readonly DispatcherTimer errorMessageTimer;

    [ObservableProperty]
    private ObservableCollection<SimpleProductCategoryModel?> productCategories = [];

    [ObservableProperty]
    private ObservableCollection<SimpleProductModel> filteredSimpleProductList; // SimpleProductModelList filtered by simpleProductListFilterText

    private ObservableCollection<SimpleProductModel> simpleProductModelList;    // unfiltered list of SimpleProductModel
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
        this.FilteredSimpleProductList = [];

        this.dialogWindow = this.serviceProvider.GetRequiredService<ProductEditorWindow>();
        this.errorMessageTimer = InitErrorMessageTimer(10);

        Task.Run(LoadAllSimpleProductsAsync);
        var productCategoriesList = Task.Run(GetProductCategoriesAsync).Result;
        this.ProductCategories = new ObservableCollection<SimpleProductCategoryModel?>(productCategoriesList);
    }

    [RelayCommand]
    private async Task LoadProductListAsync()
    {
        var response = await this.httpClientManager.GetAllSimpleProductAsync();
        ShowTemporaryErrorMessage(response.Item1);
        this.SimpleProductModelList = new ObservableCollection<SimpleProductModel>(response.Item2);

        this.logger.Information(string.Format(ConstantMessages.MainWindowViewModel_LoadProducts, this.FilteredSimpleProductList.Count));
    }

    [RelayCommand]
    private async Task AddSimpleProductAsync()
    {
        dialogWindow = this.serviceProvider.GetRequiredService<ProductEditorWindow>();

        var productViewModelDataContext = (ProductEditorViewModel)dialogWindow.DataContext;                
        productViewModelDataContext.Init(null , ProductCategories);
        var dialogResult = dialogWindow.ShowDialog() ?? false;

        if (dialogResult)
        {
            await this.httpClientManager.AddNewSimpleProductAsync(productViewModelDataContext.EditingSimpleProductModel);
            await this.LoadAllSimpleProductsAsync();
            this.logger.Information(string.Format(ConstantMessages.MainWindowViewModel_AddProduct, productViewModelDataContext.EditingSimpleProductModel.Id));
        }
    }

    [RelayCommand]
    private async Task EditSimpleProductAsync(SimpleProductModel editingSimpleProduct)
    {
        if (editingSimpleProduct is null)
        {
            return;
        }

        dialogWindow = this.serviceProvider.GetRequiredService<ProductEditorWindow>();

        var productViewModelDataContext = (ProductEditorViewModel)dialogWindow.DataContext;
        productViewModelDataContext.Init(editingSimpleProduct, ProductCategories);

        var dialogResult = dialogWindow.ShowDialog() ?? false;

        if (dialogResult)
        {            
            var errorMessage = await this.httpClientManager.UpdateSimpleProductAsync(editingSimpleProduct);
            this.logger.Information(string.Format(ConstantMessages.MainWindowViewModel_EditProduct, editingSimpleProduct.Id));
        }
    }

    [RelayCommand]
    public async Task RemoveSimpleProductAsync(SimpleProductModel removingSimpleProduct)
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
    
    private void SetFilteredSimpleProductList(string filterText)
    {
        this.FilteredSimpleProductList = 
            string.IsNullOrWhiteSpace(filterText)
            ? this.SimpleProductModelList
            : new ObservableCollection<SimpleProductModel>(
                this.SimpleProductModelList.Where(simpleProductModelList => simpleProductModelList.Name.Contains(filterText))
                .ToList());
    }

    private async Task<List<SimpleProductCategoryModel>> GetProductCategoriesAsync()
    {
        var response = await this.httpClientManager.GetAllSimpleProductCategoriesAsync();
        ShowTemporaryErrorMessage(response.Item1);
        var simpleProductCategoryModelList = response.Item2;
        
        return simpleProductCategoryModelList;
    }

    private async Task LoadAllSimpleProductsAsync()
    {
        var response = await this.httpClientManager.GetAllSimpleProductAsync();
        ShowTemporaryErrorMessage(response.Item1);
        this.SimpleProductModelList = new ObservableCollection<SimpleProductModel>(response.Item2);
    }

    private void ShowTemporaryErrorMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }
        
        ErrorMessage = $"[{DateTime.Now.ToShortTimeString()}] - {message}";
        errorMessageTimer.Stop();
        errorMessageTimer.Start();
    }

    private DispatcherTimer InitErrorMessageTimer(int timerIntervalInSeconds)
    {
        var dispatcherTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(10)
        };
        dispatcherTimer.Tick += (s, e) =>
        {
            dispatcherTimer.Stop();
            ErrorMessage = string.Empty;
        };
        
        return dispatcherTimer;
    }
}
