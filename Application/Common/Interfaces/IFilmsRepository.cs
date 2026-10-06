using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IFilmsRepository
    {
        Task<IEnumerable<Films>> GetAllAsync(CancellationToken cancellation = default); // Hay que implementarlo en Infrastructure/Persistence/Repositories
        Task<Films?> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task AddAsync(Films films, CancellationToken cancellation = default);
        Task UpdateAsync(Films films, CancellationToken cancellation = default);
    }
}
