using SimpleProductServices.Model;

namespace SimpleProductServices.Services;

public interface ISimpleProductService
{
    public Task<List<SimpleProductModel>> GetAllSimpleProductsAsync();

    public Task<SimpleProductModel?> GetSimpleProductByProductIdAsync(Guid productId);

    public Task AddSimpleProductAsync(SimpleProductModel simpleProductModel);

    public Task RemoveSimpleProductAsync(Guid productId);    
}