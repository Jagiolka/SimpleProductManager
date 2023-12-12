namespace SimpleProductManager.DataLayer;

using SimpleProductManager.DataLayer.DataModel;

public interface IHttpClientManager
{
    public Task<List<SimpleProductStockModel>> GetSimpleProductStockAsync();

    public Task AddSimpleProductAsync(SimpleProductStockModel simpleProductStockModel);

    public Task RemoveProductStockAsync(SimpleProductStockModel simpleProductStockModel);

    public Task<List<ProductCategoryModel>> GetProductCategoriesAsync();
}