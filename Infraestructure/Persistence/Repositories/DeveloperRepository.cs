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
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly ApplicationDbContext _context;

        public DeveloperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Developer>> GetAllAsync(CancellationToken cancellation = default)
        {
            return await _context.Developers
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public async Task<Developer?> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            return await _context.Developers
                .FirstOrDefaultAsync(d => d.Id == id, cancellation);
        }

        public async Task AddAsync(Developer developer, CancellationToken cancellation = default)
        {
            await _context.Developers.AddAsync(developer, cancellation);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task UpdateAsync(Developer developer, CancellationToken cancellation = default)
        {
            _context.Developers.Update(developer);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task DeleteAsync(int id)
        {
            var developer = await _context.Developers
                .FirstOrDefaultAsync(d => d.Id == id);

            if (developer == null) return;

            _context.Developers.Remove(developer);
            await _context.SaveChangesAsync();
        }

    }
}
