using Application.Services;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : Controller
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var product = await _service.GetAllProductAsync();

            if(product == null || !product.Any())
            {
                return NotFound();
            }

            return Ok(product);
        }
        catch (Exception ex)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Error interno del servidor.",
                Detail = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, problem);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _service.GetProductById(id);

        if(product == null) return NotFound(new { Message = $"No se encontro ningun producto."});

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ProductCreateDto dto,
                                            [FromServices] IValidator<ProductCreateDto> validator)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(dto);

            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            var productId = await _service.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = productId }, new { id = productId });
        }
        catch (Exception ex)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Error interno del servidor.",
                Detail = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, problem);
        }
    }


}
