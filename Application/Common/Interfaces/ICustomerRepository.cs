using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
        Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellation = default);
        Task AddAsync(Customer customer, CancellationToken cancellation = default);
        Task UpdateAsync(Customer customer, CancellationToken cancellation = default);
    }
}
