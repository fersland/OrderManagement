using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellation = default)
        {
            return await _context.Products // cuando el codigo llega al await se libera el hilo y se regresa al grupo de hilos para atender a otras peticiones, caso contrario el hilo se queda esperando lo que ocaciona una mayor lentitud en la respuesta
                .AsNoTracking() // mejor rendimiento , un menor consumo de memoria, se utiliza para una consulta de un endpoint
                .ToListAsync(cancellation);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        {
            return await _context.Products
                .FirstOrDefaultAsync(cancellation);
        }

        public async Task AddAsync(Product product, CancellationToken cancellation = default)
        {
            await _context.Products.AddAsync(product, cancellation);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellation = default)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellation);
        }
    }
}
