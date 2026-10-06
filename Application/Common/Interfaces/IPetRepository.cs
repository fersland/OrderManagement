using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllAsync(CancellationToken cancellation = default);
        Task<Pet?> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task AddAsync(Pet pet, CancellationToken cancellation = default);
        Task UpdateAsync(Pet pet, CancellationToken cancellation = default);

    }
}
