using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : Controller
{
    private readonly OrderService _service;
    private readonly ILogger _logger;

    public OrdersController(OrderService service, ILogger logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        var orderId = await _service.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = orderId }, new { id = orderId });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _service.GetOrderByIdAsync(id);
        if (order == null) return NotFound(new { Message = $"No se encontró la orden buscada." });

        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAllOrdersAsync();
        return Ok(orders);
    }
}
