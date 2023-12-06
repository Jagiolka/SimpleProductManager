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

    [HttpPut(Name = "SimpleProductContext/Init")]
    [SwaggerOperation("InitDemoDatabase")]
    public async Task InitDemoDatabaseAsync()
    {
        await this.simpleProductService.InitDemoDatabaseAsync();
    }

    [HttpGet(Name = "SimpleProductContext/GetAll")]
    [SwaggerOperation("GetSimpleProducts")]
    public async Task<List<SimpleProductStockModel>> GetSimpleProductsAsync()
    {
        return await this.simpleProductService.GetSimpleProductStocksAsync();
    }

    [HttpPost(Name = "AddNewSimpleProduct")]
    [SwaggerOperation("AddNewSimpleProduct")]
    public async Task AddNewSimpleProductAsync(SimpleProductModel simpleProductModel)
    {
        await this.simpleProductService.AddNewSimpleProductAsync(simpleProductModel);
    }

    [HttpDelete(Name = "SimpleProductContext/Remove/{simpleProductId}")]
    [SwaggerOperation("RemoveSimpleProduct")]
    public async Task RemoveSimpleProductAsync(Guid simpleProductId)
    {
        await this.simpleProductService.RemoveSimpleProductAsync(simpleProductId);
    }
}