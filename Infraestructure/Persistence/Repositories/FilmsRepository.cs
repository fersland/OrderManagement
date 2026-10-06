using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class FilmsRepository : IFilmsRepository
    {
        private readonly ApplicationDbContext _context;

        public FilmsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Films>> GetAllAsync(CancellationToken cancellation = default)
        {
            return await _context.Films
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public async Task<Films?> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            return await _context
                .Films.FirstOrDefaultAsync(f => f.Id == id, cancellation);
        }

        public async Task AddAsync(Films films, CancellationToken cancellation = default)
        {
            await _context.Films.AddAsync(films, cancellation);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task UpdateAsync(Films films, CancellationToken cancellation = default)
        {
            _context.Films.Update(films);
            await _context.SaveChangesAsync(cancellation);
        }

    }
}
