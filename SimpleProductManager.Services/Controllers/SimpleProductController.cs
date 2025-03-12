using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SimpleProductServices.Model;
using SimpleProductServices.Services;

using ILogger = Serilog.ILogger;
using SimpleProductManager.Services.Entities;

namespace SimpleProductServices.Controllers;

[ApiController]
[Route("[controller]")]
[SwaggerTag]
public class SimpleProductController(ILogger logger, ISimpleProductService simpleProductService) : ControllerBase
{
    [HttpGet("GetAll")]
    [SwaggerOperation("GetAllSimpleProducts")]
    public async Task<IActionResult> GetAllSimpleProductsAsync()
    {
        try
        {
            var serviceResult = await simpleProductService.GetAllSimpleProductsAsync();
            return Ok(serviceResult);
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("GetBySimpleProductId")]
    [SwaggerOperation("GetSimpleProduct")]
    public async Task<IActionResult> GetSimpleProductsAsync(Guid simpleProductId)
    {
        try
        {
            var serviceResult = await simpleProductService.GetSimpleProductByProductIdAsync(simpleProductId);
            return Ok(serviceResult);
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("Add")]
    [SwaggerOperation("AddSimpleProduct")]
    public async Task<IActionResult> AddSimpleProductAsync([FromBody] SimpleProductInputModel SimpleProductInputModel)
    {
        try
        {
            await simpleProductService.AddSimpleProductAsync(SimpleProductInputModel);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("RemoveBySimpleProductId")]
    [SwaggerOperation("RemoveSimpleProduct")]
    public async Task<IActionResult> RemoveSimpleProductAsync(Guid simpleProductId)
    {
        try
        {
            await simpleProductService.RemoveSimpleProductAsync(simpleProductId);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
            return StatusCode(500, ex.Message);
        }
    }
}
public record SimpleProductInputModel(string SimpleProductName, string SimpleProductDescription, Guid SimpleProductCategoryId, decimal SimpleProductPrice);
