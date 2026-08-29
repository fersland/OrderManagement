using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : Controller
{
    private readonly CustomerSerivce _service;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(CustomerSerivce service, ILogger<CustomersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customer = await _service.GetAllCustomerAsync();
        return Ok(customer);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _service.GetOrderByIdAsync(id);
        if (customer == null) return NotFound(new { Message = $"No se encontro algun cliente." });

        return Ok(customer);
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateDtoCustomer dto,
                                            [FromServices] IValidator<CreateDtoCustomer> validator)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Error en la entrada de datos.");
                return BadRequest(validationResult.ToDictionary());
            }

            var customerId = await _service.CreateCustomerAsync(dto);
            _logger.LogInformation("Se guardo el nuevo cliente.");
            return CreatedAtAction(nameof(GetById), new { id = customerId }, new { id = customerId });
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
