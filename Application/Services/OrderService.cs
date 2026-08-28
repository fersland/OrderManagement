using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public record CreateOrderItemDto(Guid ProductId, string ProductName, decimal UnitPrice, int quantity);
public record CreateOrderDto(string CustomerName, string CustomerEmail, List<CreateOrderItemDto> Items);
public record OrderItemDto(Guid Id, Guid ProductId, string ProductName, decimal UnitPrice, int Quantity);
public record OrderDto(Guid Id, string CustomerName, string CustomerEmail, DateTime CreatedAt, string Status, decimal TotalAmound, List<OrderItemDto> Items);


public class OrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateOrderAsync(CreateOrderDto dto, CancellationToken cancellation = default)
    {
        var order = new Order(dto.CustomerName, dto.CustomerEmail);

        foreach(var item in dto.Items)
        {
            order.AddItem(item.ProductId, item.ProductName, item.UnitPrice, item.quantity);
        }

        await _repository.AddAsync(order, cancellation);
        return order.Id;
    }

    public async Task<OrderDto?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetByIdAsync(id, cancellationToken);
        if (order is null) return null;

        return MapToDto(order);
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        var orders = await _repository.GetAllAsync(cancellationToken);
        return orders.Select(MapToDto);
    }

    private static OrderDto MapToDto(Order order) => new(
        order.Id,
        order.CustomerName,
        order.CustomerEmail,
        order.CreatedAt,
        order.Status.ToString(),
        order.TotalAmount,
        order.items.Select(i => new OrderItemDto(i.Id, i.ProductId, i.ProductName, i.UnitPrice, i.Quantity)).ToList()
    );


}
