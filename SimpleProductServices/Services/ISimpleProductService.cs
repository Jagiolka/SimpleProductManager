namespace SimpleProductServices.Services;

using SimpleProductManager.DataLayer.DataModel;

public interface ISimpleProductService
{
    public Task InitDemoDatabaseAsync();

    public Task<List<SimpleProductStockModel>> GetSimpleProductStocksAsync();

    public Task AddNewSimpleProductAsync(SimpleProductModel simpleProduct);

    public Task RemoveSimpleProductAsync(Guid simpleProductId);
}