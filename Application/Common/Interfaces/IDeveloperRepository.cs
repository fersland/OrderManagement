using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDeveloperRepository
    {
        Task<IEnumerable<Developer>> GetAllAsync(CancellationToken cancellation = default);
        Task<Developer?> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task AddAsync(Developer developer, CancellationToken cancellation = default);
        Task UpdateAsync(Developer developer, CancellationToken cancellation = default);
        Task DeleteAsync(int id);
    }
}
