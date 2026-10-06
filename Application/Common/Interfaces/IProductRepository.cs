using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellation = default);
        Task AddAsync(Product product, CancellationToken cancellation = default);
        Task UpdateAsync(Product product, CancellationToken cancellation = default);
    }
}
