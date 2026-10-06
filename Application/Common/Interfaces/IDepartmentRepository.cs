using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellation = default);
        Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
        Task AddAsync(Department department, CancellationToken cancellation = default);
    }
}
