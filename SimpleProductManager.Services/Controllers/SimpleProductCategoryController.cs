using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using SimpleProductServices.Model;
using SimpleProductServices.Services;

using ILogger = Serilog.ILogger;


namespace SimpleProductServices.Controllers;

[ApiController]
[Route("[controller]")]
[SwaggerTag]
public class SimpleProductCategoryController(ILogger logger, ISimpleProductCategoryService simpleProductCategoryService) : ControllerBase
{
    /// <summary>
    /// Retrieves all simple ProductCategories.
    /// </summary>
    /// <returns>A list of all simple ProductCategories.</returns>
    /// <response code="200">Returns the list of simple ProductCategories.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("GetAll")]
    [SwaggerOperation("GetAllProductCategories")]
    [ProducesResponseType(typeof(SimpleProductModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Retrieves a simple ProductCategory by its SimpleProductCategoryId.
    /// </summary>
    /// <param name="productCategoryId">The unique identifier of the simple ProductCategory.</param>
    /// <returns>The simple ProductCategory with the specified ID.</returns>
    /// <response code="200">Returns the requested simple ProductCategory.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("GetByProductCategoryId")]
    [SwaggerOperation("GetProductCategoriesByProductCategoryId")]
    [ProducesResponseType(typeof(SimpleProductModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Adds a new simple ProductCategory.
    /// </summary>
    /// <param name="productCategoryName">The name of the simple ProductCategory to add.</param>
    /// <returns>The new created simple ProductCategory.</returns>
    /// <response code="200">Returns the new created simple ProductCategory.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost("Add")]
    [SwaggerOperation("AddProductCategory")]
    [ProducesResponseType(typeof(SimpleProductModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Removes a simple ProductCategory by SimpleProductCategoryId.
    /// </summary>
    /// <param name="simpleProductCategoryId">The unique identifier of the simple ProductCategory to remove.</param>
    /// <returns>No content.</returns>
    /// <response code="200">If the simple ProductCategory was removed successfully.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpDelete("RemoveByProductCategoryId")]
    [SwaggerOperation("RemoveProductCategory")]
    [ProducesResponseType(typeof(SimpleProductModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveProductCategoryAsync(Guid simpleProductCategoryId)
    {
        try
        {
            await simpleProductCategoryService.RemoveSimpleProductCategoryAsync(simpleProductCategoryId);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
            return StatusCode(500, ex.Message);
        }
    }
}
