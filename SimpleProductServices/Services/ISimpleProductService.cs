﻿namespace SimpleProductServices.Services;

using SimpleProductManager.DataLayer.DataModel;

public interface ISimpleProductService
{
    public Task InitDemoDatabaseAsync();

    public Task<List<SimpleProductStockModel>> GetSimpleProductStocksAsync();

    public Task AddSimpleProductStockAsync(SimpleProductStockModel simpleProductStock);

    public Task RemoveSimpleProductStockAsync(Guid simpleProductId);

    public Task<List<ProductCategoryModel>> GetProductCategoriesAsync();
}