using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Department department, CancellationToken cancellation = default)
    {
        await _context.Departments.AddAsync(department, cancellation);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellation = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .ToListAsync(cancellation);
    }

    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellation = default)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(cancellation);
    }
}
