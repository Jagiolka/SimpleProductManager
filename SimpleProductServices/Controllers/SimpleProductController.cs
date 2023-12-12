namespace SimpleProductServices.Controllers;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using SimpleProductManager.DataLayer.DataModel;
using SimpleProductServices.Services;

[ApiController]
[Route("[controller]")]
[SwaggerTag]
public class SimpleProductController : ControllerBase
{
    private readonly ILogger<SimpleProductController> _logger;
    private readonly ISimpleProductService simpleProductService;

    public SimpleProductController(ILogger<SimpleProductController> logger, ISimpleProductService simpleProductService)
    {
        _logger = logger;
        this.simpleProductService = simpleProductService;
    }

    [HttpPut("Init")]
    [SwaggerOperation("InitDemoDatabase")]
    public async Task InitDemoDatabaseAsync()
    {
        await this.simpleProductService.InitDemoDatabaseAsync();
    }

    [HttpGet("GetAll")]
    [SwaggerOperation("GetSimpleProducts")]
    public async Task<List<SimpleProductStockModel>> GetSimpleProductsAsync()
    {
        return await this.simpleProductService.GetSimpleProductStocksAsync();
    }

    [HttpPost("Add")]
    [SwaggerOperation("AddSimpleProduct")]
    public async Task AddSimpleProductAsync(SimpleProductStockModel simpleProductStockModel)
    {
        await this.simpleProductService.AddSimpleProductStockAsync(simpleProductStockModel);
    }

    [HttpDelete("Remove/{simpleProductId}")]
    [SwaggerOperation("RemoveSimpleProduct")]
    public async Task RemoveSimpleProductAsync(Guid simpleProductId)
    {
        await this.simpleProductService.RemoveSimpleProductStockAsync(simpleProductId);
    }

    [HttpGet("GetProductCategories")]
    public async Task<List<ProductCategoryModel>> GetProductCategoriesAsync()
    {
        return await this.simpleProductService.GetProductCategoriesAsync();
    }
}
