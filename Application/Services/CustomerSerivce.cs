using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR.Pipeline;

namespace Application.Services;

public record CustomerDto(Guid Id, string Ci, string FirstName, string LastName, string Phone, string Email);
public record CreateDtoCustomer(Guid Id, string Ci, string FirstName, string LastName, string Phone, string Email);


public class CustomerSerivce
{
    private readonly ICustomerRepository _repository;

    public CustomerSerivce(ICustomerRepository repository)
    {
        _repository = repository;
    } 

    private static CustomerDto MapToDto(Customer customer) => new(
        customer.Id,
        customer.Ci,
        customer.FirstName,
        customer.LastName,
        customer.Phone,
        customer.Email
        );

    public async Task<IEnumerable<CustomerDto>> GetAllCustomerAsync(CancellationToken cancellation = default)
    {
        var customers = await _repository.GetAllAsync(cancellation);
        return customers.Select(MapToDto);
    }

    public async Task<CustomerDto?> GetOrderByIdAsync(Guid id, CancellationToken cancellation = default)
    {
        var customer = await _repository.GetByIdAsync(id, cancellation);
        if (customer == null) return null;

        return MapToDto(customer);
    }

    public async Task<Guid> CreateCustomerAsync(CreateDtoCustomer dto, CancellationToken cancellation = default)
    {
        var customer = new Customer(dto.Id, dto.Ci, dto.FirstName, dto.LastName, dto.Phone, dto.Email);

        await _repository.AddAsync(customer);
        return customer.Id;
    }


}
