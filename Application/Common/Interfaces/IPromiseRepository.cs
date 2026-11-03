using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPromiseRepository
    {
        public Task<IEnumerable<Promise>> GetAllAsync(CancellationToken cancellation = default);
        public Task<Promise?> GetByIdAsync(int id, CancellationToken cancellation = default);
        public Task AddAsync(Promise promise, CancellationToken cancellation = default);
        public Task UpdateAsync(Promise promise, CancellationToken cancellation = default);
        public Task DeleteAsync(int id);
    }
}
