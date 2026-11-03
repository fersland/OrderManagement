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
    public class PromiseRepository : IPromiseRepository
    {
        private readonly ApplicationDbContext _context;

        public PromiseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Promise>> GetAllAsync(CancellationToken cancellation = default)
        {
            return await _context.Promises
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public Task<Promise?> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            return _context.Promises
                .FirstOrDefaultAsync(x => x.Id == id, cancellation);
        }

        public async Task AddAsync(Promise promise, CancellationToken cancellation = default)
        {
            await _context.Promises.AddAsync(promise, cancellation);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task UpdateAsync(Promise promise, CancellationToken cancellation = default)
        {
            _context.Promises.Update(promise);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _context.Promises.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null) return;

            _context.Promises.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
