using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using SimpleProductServices.Model;
using SimpleProductServices.Services;

using ILogger = Serilog.ILogger;

namespace SimpleProductServices.Controllers;

[ApiController]
[Route("[controller]")]
[SwaggerTag]
public class SimpleProductController(ILogger logger, ISimpleProductService simpleProductService) : ControllerBase
{
    /// <summary>
    /// Retrieves all SimpleProducts.
    /// </summary>
    /// <returns>A list of all simple products.</returns>
    /// <response code="200">Returns the list of SimpleProducts.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("GetAll")]
    [SwaggerOperation("GetAllSimpleProducts")]
    [ProducesResponseType(typeof(SimpleProductModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Retrieves a SimpleProduct by ProductCategoryId.
    /// </summary>
    /// <param name="simpleProductId">The unique identifier of the simple product.</param>
    /// <returns>The SimpleProduct with the specified Id.</returns>
    /// <response code="200">Returns the requested SimpleProduct.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("GetBySimpleProductId")]
    [SwaggerOperation("GetSimpleProduct")]
    [ProducesResponseType(typeof(SimpleProductModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Adds a new SimpleProduct.
    /// </summary>
    /// <param name="SimpleProductModel">The simple product data to add.</param>
    /// <returns>No content.</returns>
    /// <response code="200">If the SimpleProduct was added successfully.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpPost("Add")]
    [SwaggerOperation("AddSimpleProduct")]
    [ProducesResponseType(typeof(SimpleProductModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddSimpleProductAsync([FromBody] SimpleProductModel SimpleProductModel)
    {
        try
        {
            await simpleProductService.AddSimpleProductAsync(SimpleProductModel);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.Error(ex.ToString());
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Removes a SimpleProduct by its SimpleProductId.
    /// </summary>
    /// <param name="simpleProductId">The unique identifier of the SimpleProduct to remove.</param>
    /// <returns>No content.</returns>
    /// <response code="200">If the simple product was removed successfully.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpDelete("RemoveBySimpleProductId")]
    [SwaggerOperation("RemoveSimpleProduct")]
    [ProducesResponseType(typeof(SimpleProductModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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
