namespace SimpleProductServices.Services;

using Microsoft.EntityFrameworkCore;
using SimpleProductManager.DataLayer.DataModel;
using SimpleProductServices.Entities;
using SimpleProductServices.Extensions;

public class SimpleProductService : ISimpleProductService
{
    private readonly ILogger<SimpleProductService> logger;

    public readonly SimpleProductContext dbContext;

    public SimpleProductService(ILogger<SimpleProductService> logger, SimpleProductContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    public async Task InitDemoDatabaseAsync()
    {
        var category01 = new ProductCategory { Id = Guid.NewGuid(), Name = "Brot" };
        var category02 = new ProductCategory { Id = Guid.NewGuid(), Name = "Kuchen" };
        var category03 = new ProductCategory { Id = Guid.NewGuid(), Name = "Getränk" };
        this.dbContext.ProductCategories.Add(category01);
        this.dbContext.ProductCategories.Add(category02);
        this.dbContext.ProductCategories.Add(category03);

        var product01 = new SimpleProduct { Id = Guid.NewGuid(), Name = "Weißbrot", ProductCategories = new ProductCategory[] { category01 }.ToList() };
        var product02 = new SimpleProduct { Id = Guid.NewGuid(), Name = "Vollkornbrot", ProductCategories = new ProductCategory[] { category01 }.ToList() };
        var product03 = new SimpleProduct { Id = Guid.NewGuid(), Name = "Erdbeerkuchen", ProductCategories = new ProductCategory[] { category02 }.ToList() };
        var product04 = new SimpleProduct { Id = Guid.NewGuid(), Name = "Kaffee", ProductCategories = new ProductCategory[] { category03 }.ToList() };
        var product05 = new SimpleProduct { Id = Guid.NewGuid(), Name = "kakao", ProductCategories = new ProductCategory[] { category03 }.ToList() };
        this.dbContext.SimpleProducts.Add(product01);
        this.dbContext.SimpleProducts.Add(product02);
        this.dbContext.SimpleProducts.Add(product03);
        this.dbContext.SimpleProducts.Add(product04);
        this.dbContext.SimpleProducts.Add(product05);

        this.dbContext.SimpleProductStocks.Add(new SimpleProductStock { SimpleProductId = Guid.NewGuid(), SimpleProduct = product01, Quantity = 12});
        this.dbContext.SimpleProductStocks.Add(new SimpleProductStock { SimpleProductId = Guid.NewGuid(), SimpleProduct = product02, Quantity = 6 });
        this.dbContext.SimpleProductStocks.Add(new SimpleProductStock { SimpleProductId = Guid.NewGuid(), SimpleProduct = product03, Quantity = 10 });
        this.dbContext.SimpleProductStocks.Add(new SimpleProductStock { SimpleProductId = Guid.NewGuid(), SimpleProduct = product04, Quantity = 25 });
        this.dbContext.SimpleProductStocks.Add(new SimpleProductStock { SimpleProductId = Guid.NewGuid(), SimpleProduct = product05, Quantity = 8 });

        await this.dbContext.SaveChangesAsync();

        this.logger.Log(LogLevel.Information, "Initialized Demo data");
    }

    public async Task<List<SimpleProductStockModel>> GetSimpleProductStocksAsync()
    {
        var result = await this.dbContext.SimpleProductStocks
          .AsNoTracking()
          .Select(stock => new SimpleProductStock() { SimpleProduct = stock.SimpleProduct, Quantity = stock.Quantity })
          .ToListAsync();

        return result.Select(simpleProductStock => simpleProductStock.MapToSimpleProductStockModel()).ToList();
    }

    public async Task AddNewSimpleProductAsync(SimpleProductModel simpleProduct)
    {
        this.dbContext.SimpleProducts.Add(simpleProduct.MapToSimpleProductModel());
        await this.dbContext.SaveChangesAsync();

        this.logger.Log(LogLevel.Information, $"Add new product: {simpleProduct.Id}");
    }

    public async Task RemoveSimpleProductAsync(Guid simpleProductId)
    {
        var removingSimpleProduct = await this.dbContext.SimpleProducts.FindAsync(simpleProductId);

        if (removingSimpleProduct is null) 
        {
            this.logger.LogError($"no fount with product id: {simpleProductId}");
            throw new ArgumentNullException(nameof(removingSimpleProduct));
        }

        this.dbContext.Remove(removingSimpleProduct);
        await this.dbContext.SaveChangesAsync();
        this.logger.Log(LogLevel.Information, $"remove product: {simpleProductId}");
    }
}
