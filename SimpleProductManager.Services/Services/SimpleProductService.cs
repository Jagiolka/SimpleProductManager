using SimpleProductManager.Data.Entities;
using SimpleProductServices.Model;

namespace SimpleProductServices.Services;

using Microsoft.EntityFrameworkCore;
using SimpleProductServices.Extensions;

public class SimpleProductService : ISimpleProductService
{
    private readonly ILogger<SimpleProductService> logger;

    public readonly SimpleProductDatabaseContext DbDatabaseContext;

    public SimpleProductService(ILogger<SimpleProductService> logger, SimpleProductDatabaseContext dbDatabaseContext)
    {
        this.logger = logger;
        this.DbDatabaseContext = dbDatabaseContext;
    }

    public async Task InitDemoDatabaseAsync()
    {
        var category01 = new ProductCategory { Id = Guid.NewGuid(), Name = "Brot" };
        var category02 = new ProductCategory { Id = Guid.NewGuid(), Name = "Kuchen" };
        var category03 = new ProductCategory { Id = Guid.NewGuid(), Name = "Getränk" };
        this.DbDatabaseContext.ProductCategories.Add(category01);
        this.DbDatabaseContext.ProductCategories.Add(category02);
        this.DbDatabaseContext.ProductCategories.Add(category03);

        var product01 = new SimpleProduct { Id = Guid.NewGuid(), Name = "Weißbrot", ProductCategories = new ProductCategory[] { category01 }.ToList() };
        var product02 = new SimpleProduct { Id = Guid.NewGuid(), Name = "Vollkornbrot", ProductCategories = new ProductCategory[] { category01 }.ToList() };
        var product03 = new SimpleProduct { Id = Guid.NewGuid(), Name = "Erdbeerkuchen", ProductCategories = new ProductCategory[] { category02 }.ToList() };
        var product04 = new SimpleProduct { Id = Guid.NewGuid(), Name = "Kaffee", ProductCategories = new ProductCategory[] { category03 }.ToList() };
        var product05 = new SimpleProduct { Id = Guid.NewGuid(), Name = "kakao", ProductCategories = new ProductCategory[] { category03 }.ToList() };
        this.DbDatabaseContext.SimpleProducts.Add(product01);
        this.DbDatabaseContext.SimpleProducts.Add(product02);
        this.DbDatabaseContext.SimpleProducts.Add(product03);
        this.DbDatabaseContext.SimpleProducts.Add(product04);
        this.DbDatabaseContext.SimpleProducts.Add(product05);

        this.DbDatabaseContext.SimpleProductStocks.Add(new SimpleProductStock { SimpleProductId = Guid.NewGuid(), SimpleProduct = product01, Quantity = 12});
        this.DbDatabaseContext.SimpleProductStocks.Add(new SimpleProductStock { SimpleProductId = Guid.NewGuid(), SimpleProduct = product02, Quantity = 6 });
        this.DbDatabaseContext.SimpleProductStocks.Add(new SimpleProductStock { SimpleProductId = Guid.NewGuid(), SimpleProduct = product03, Quantity = 10 });
        this.DbDatabaseContext.SimpleProductStocks.Add(new SimpleProductStock { SimpleProductId = Guid.NewGuid(), SimpleProduct = product04, Quantity = 25 });
        this.DbDatabaseContext.SimpleProductStocks.Add(new SimpleProductStock { SimpleProductId = Guid.NewGuid(), SimpleProduct = product05, Quantity = 8 });

        await this.DbDatabaseContext.SaveChangesAsync();

        this.logger.Log(LogLevel.Information, "Initialized Demo data");
    }

    public async Task<List<SimpleProductStockModel>> GetSimpleProductStocksAsync()
    {
        var result = await this.DbDatabaseContext.SimpleProductStocks
            .Include(stock => stock.SimpleProduct)
            .ThenInclude(product => product.ProductCategories)
            .AsNoTracking()
            .Select(stock => new SimpleProductStock() { SimpleProduct = stock.SimpleProduct, Quantity = stock.Quantity })
            .ToListAsync();

        return result.Select(simpleProductStock => simpleProductStock.MapToSimpleProductStockModel()).ToList();
    }
    
    public async Task AddSimpleProductStockAsync(SimpleProductStockModel simpleProductStock)
    {
        var productStockEntity = simpleProductStock.MapToSimpleProductStockModel();
        this.DbDatabaseContext.SimpleProductStocks.Add(productStockEntity);
        await this.DbDatabaseContext.SaveChangesAsync();

        this.logger.Log(LogLevel.Information, $"Add new product: {simpleProductStock.SimpleProductModelId}");
    }

    public async Task RemoveSimpleProductStockAsync(Guid simpleProductId)
    {
        var removingSimpleProductStock = 
            await this.DbDatabaseContext.SimpleProductStocks
            .Include(stock => stock.SimpleProduct)
            .ThenInclude(product => product.ProductCategories)
            .Where(stock => stock.SimpleProductId == simpleProductId)
            .FirstAsync();

        if (removingSimpleProductStock is null) 
        {
            this.logger.LogError($"no product found with id: {simpleProductId}");
            throw new ArgumentNullException(nameof(removingSimpleProductStock));
        }

        this.DbDatabaseContext.Remove(removingSimpleProductStock);

        await this.DbDatabaseContext.SaveChangesAsync();
        this.logger.Log(LogLevel.Information, $"remove product: {simpleProductId}");
    }

    public async Task<List<ProductCategoryModel>> GetProductCategoriesAsync() 
    {
        var result = await this.DbDatabaseContext.ProductCategories
          .AsNoTracking()
          .Select(category => new ProductCategoryModel(category.Id, category.Name))
          .ToListAsync();

        return result;
    }
}
