using SimpleProductServices.Model;
using SimpleProductManager.Services;
using Microsoft.EntityFrameworkCore;
using SimpleProductServices.Extensions;
using SimpleProductManager.Services.Entities;

namespace SimpleProductServices.Services;

public class SimpleProductCategoryService(ILogger<SimpleProductService> logger, SimpleProductDatabaseContext dbContext) : ISimpleProductCategoryService
{
    public async Task<List<SimpleProductCategoryModel>> GetAllSimpleProductCategoriesAsync()
    {
        return await dbContext.SimpleProductCategories
            .Select(spc => spc.MapToCategoryModel())
            .ToListAsync();
    }

    public async Task<SimpleProductCategoryModel?> GetSimpleProductCategoryByProductCategoryIdAsync(Guid productCategoryId)
    {
        var result = await dbContext
            .SimpleProductCategories
            .FirstOrDefaultAsync(pc => pc.Id == productCategoryId);
        return result?.MapToCategoryModel();
    }

    public async Task<SimpleProductCategoryModel> AddSimpleProductCategoryAsync(string productCategoryName)
    {
        if (await dbContext.SimpleProductCategories.AnyAsync(spc => spc.Name == productCategoryName))
        {
            string simpleProductCategoryAlreadyExist = $"SimpleProductCategory with the Name \'{productCategoryName}\' already exist.";
            logger.LogError(simpleProductCategoryAlreadyExist);
            throw new Exception(simpleProductCategoryAlreadyExist);
        }

        var newSimpleProductCategory = new SimpleProductCategory()
        {
            Id = Guid.NewGuid(),
            Name = productCategoryName
        };

        dbContext.SimpleProductCategories.Add(newSimpleProductCategory);
        await dbContext.SaveChangesAsync();

        return newSimpleProductCategory.MapToCategoryModel();
    }

    public async Task RemoveSimpleProductCategoryAsync(Guid productCategoryId)
    {
        var productCategory = await dbContext.SimpleProductCategories.FirstOrDefaultAsync(pc => pc.Id == productCategoryId);

        if (await dbContext.SimpleProductCategories.FindAsync(productCategoryId) is null)
        {
            throw new Exception($"No SimpleProductCategory found with ProductCategoryId: \'{productCategoryId}\'");
        }

        dbContext.SimpleProductCategories.Remove(productCategory!);
        await dbContext.SaveChangesAsync();
    }
}
