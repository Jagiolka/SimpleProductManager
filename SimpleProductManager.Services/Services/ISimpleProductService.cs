using SimpleProductServices.Controllers;
using SimpleProductServices.Model;

namespace SimpleProductServices.Services;

public interface ISimpleProductService
{
    public Task<List<SimpleProductModel>> GetAllSimpleProductsAsync();

    public Task<SimpleProductModel?> GetSimpleProductByProductIdAsync(Guid productId);

    public Task AddSimpleProductAsync(SimpleProductInputModel simpleProductInputModel);

    public Task RemoveSimpleProductAsync(Guid productId);    
}