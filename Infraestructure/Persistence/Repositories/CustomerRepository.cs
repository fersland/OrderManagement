using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellation = default)
    {
        return await _context.Customers
            .AsNoTracking()
            .ToListAsync(cancellation);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellation = default)
    {
        await _context.Customers.AddAsync(customer, cancellation);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellation = default)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(cancellation);
    }

    public Task UpdateAsync(Customer customer, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }
}
