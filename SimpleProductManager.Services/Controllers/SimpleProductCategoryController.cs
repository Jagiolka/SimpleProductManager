using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SimpleProductServices.Services;

using ILogger = Serilog.ILogger;

namespace SimpleProductServices.Controllers;

[ApiController]
[Route("[controller]")]
[SwaggerTag]
public class SimpleProductCategoryController(ILogger logger, ISimpleProductCategoryService simpleProductCategoryService) : ControllerBase
{
    [HttpGet("GetAll")]
    [SwaggerOperation("GetAllProductCategories")]
    public async Task<IActionResult> GetAllProductCategoriesAsync()
    {
        try
        {
            var serviceResult = await simpleProductCategoryService.GetAllSimpleProductCategoriesAsync();
            return Ok(serviceResult);
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("GetByProductCategoryId")]
    [SwaggerOperation("GetProductCategoriesByProductCategoryId")]
    public async Task<IActionResult> GetProductCategoryByProductCategoryIdAsync(Guid productCategoryId)
    {
        try
        {
            var serviceResult = await simpleProductCategoryService.GetSimpleProductCategoryByProductCategoryIdAsync(productCategoryId);
            return Ok(serviceResult);
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("Add")]
    [SwaggerOperation("AddProductCategory")]
    public async Task<IActionResult> AddProductCategoryAsync(string productCategoryName)
    {
        try
        {
            var serviceResult = await simpleProductCategoryService.AddSimpleProductCategoryAsync(productCategoryName);
            return Ok(serviceResult);
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
            return StatusCode(500, ex.Message);
        }        
    }

    [HttpDelete("RemoveByProductCategoryId")]
    [SwaggerOperation("RemoveProductCategory")]
    public async Task<IActionResult> RemoveProductCategoryAsync(Guid productCategoryId)
    {
        try
        {
            await simpleProductCategoryService.RemoveSimpleProductCategoryAsync(productCategoryId);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
            return StatusCode(500, ex.Message);
        }
    }
}
