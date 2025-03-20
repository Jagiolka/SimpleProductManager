using SimpleProductServices.Model;
using SimpleProductManager.Services;
using Microsoft.EntityFrameworkCore;
using SimpleProductServices.Extensions;
using SimpleProductManager.Services.Entities;

namespace SimpleProductServices.Services;

public class SimpleProductService(
    ILogger<SimpleProductService> logger, 
    SimpleProductDatabaseContext dbContext) : ISimpleProductService
{
    public async Task<List<SimpleProductModel>> GetAllSimpleProductsAsync()
    {
        return await dbContext.SimpleProducts
            .Include(sp => sp.SimpleProductCategory)
            .AsNoTracking()
            .Select(x => x.MapToSimpleProductModel())
            .ToListAsync();        
    }

    public async Task<SimpleProductModel?> GetSimpleProductByProductIdAsync(Guid simpleProductId)
    {
        SimpleProduct? result = await GetSimpleProductById(dbContext, simpleProductId);

        return result?.MapToSimpleProductModel();
    }
    
    public async Task AddSimpleProductAsync(SimpleProductModel simpleProductModel)
    {
        if(simpleProductModel.SimpleProductCategory is null) 
        {
            throw new Exception($"No SimpleProductCategory in SimpleProduct \'{simpleProductModel.Id}\'.");
        }

        var simpleProductCategoryEntity = await dbContext.SimpleProductCategories.FirstOrDefaultAsync(spc => spc.Id == simpleProductModel.SimpleProductCategory.Id);
        if (simpleProductCategoryEntity is null) 
        {
            throw new Exception($"No SimpleProductCategory with SimpleProductCategoryId \'{simpleProductModel.SimpleProductCategory}\' does not exist.");
        }

        var newProduct = simpleProductModel.MapToSimpleProduct(simpleProductCategoryEntity);
        dbContext.SimpleProducts.Add(newProduct);
        await dbContext.SaveChangesAsync();

        logger.Log(LogLevel.Information, $"Product successfully added. SimpleProductId: {newProduct.Id}");
    }

    public async Task RemoveSimpleProductAsync(Guid simpleProductId)
    {
        var removingSimpleProduct = await GetSimpleProductById(dbContext, simpleProductId);
        if (removingSimpleProduct is null)
        {
            string productNotFoundMessage = $"no simpleProduct found with id: {simpleProductId}";
            logger.LogError(productNotFoundMessage);
            throw new Exception(productNotFoundMessage);
        }

        dbContext.Remove(removingSimpleProduct);
        await dbContext.SaveChangesAsync();

        logger.Log(LogLevel.Information, $"SimpleProduct successfully removed. SimpleProductId: {simpleProductId}");
    }

    private static async Task<SimpleProduct?> GetSimpleProductById(SimpleProductDatabaseContext dbContext, Guid simpleProductId)
    {
        return await dbContext.SimpleProducts
            .Include(sp => sp.SimpleProductCategory)
            .AsNoTracking()
            .FirstOrDefaultAsync(sp => sp.Id == simpleProductId);
    }
}