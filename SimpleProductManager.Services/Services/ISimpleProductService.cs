using SimpleProductServices.Model;

namespace SimpleProductServices.Services;

public interface ISimpleProductService
{
    public Task InitDemoDatabaseAsync();

    public Task<List<SimpleProductStockModel>> GetSimpleProductStocksAsync();

    public Task AddSimpleProductStockAsync(SimpleProductStockModel simpleProductStock);

    public Task RemoveSimpleProductStockAsync(Guid simpleProductId);

    public Task<List<ProductCategoryModel>> GetProductCategoriesAsync();
}