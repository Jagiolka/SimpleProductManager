using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SimpleProductServices.Model;
using SimpleProductServices.Services;

using ILogger = Serilog.ILogger;

namespace SimpleProductServices.Controllers;

[ApiController]
[Route("[controller]")]
[SwaggerTag]
public class SimpleProductController(
    ILogger logger,
    ISimpleProductService simpleProductService)
    : ControllerBase
{
    [HttpPut("Init")]
    [SwaggerOperation("InitDemoDatabase")]
    public async Task InitDemoDatabaseAsync()
    {
        await simpleProductService.InitDemoDatabaseAsync();
    }

    [HttpGet("GetAll")]
    [SwaggerOperation("GetSimpleProducts")]
    public async Task<List<SimpleProductStockModel>> GetSimpleProductsAsync()
    {
        return await simpleProductService.GetSimpleProductStocksAsync();
    }

    [HttpPost("Add")]
    [SwaggerOperation("AddSimpleProduct")]
    public async Task AddSimpleProductAsync(SimpleProductStockModel simpleProductStockModel)
    {
        await simpleProductService.AddSimpleProductStockAsync(simpleProductStockModel);
    }

    [HttpDelete("Remove/{simpleProductId}")]
    [SwaggerOperation("RemoveSimpleProduct")]
    public async Task RemoveSimpleProductAsync(Guid simpleProductId)
    {
        await simpleProductService.RemoveSimpleProductStockAsync(simpleProductId);
    }

    [HttpGet("GetProductCategories")]
    public async Task<List<ProductCategoryModel>> GetProductCategoriesAsync()
    {
        return await simpleProductService.GetProductCategoriesAsync();
    }
}
