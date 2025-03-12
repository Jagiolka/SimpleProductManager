using SimpleProductServices.Model;

namespace SimpleProductServices.Services;

public interface ISimpleProductCategoryService
{
    public Task<List<SimpleProductCategoryModel>> GetAllSimpleProductCategoriesAsync();

    public Task<SimpleProductCategoryModel?> GetSimpleProductCategoryByProductCategoryIdAsync(Guid productCategoryId);

    public Task<SimpleProductCategoryModel> AddSimpleProductCategoryAsync(string productCategoryName);

    public Task RemoveSimpleProductCategoryAsync(Guid productCategoryId);
}