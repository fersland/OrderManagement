using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _context;

        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync(CancellationToken cancellation = default)
        {
            return await _context.Pets
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public async Task<Pet?> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            return await _context.Pets
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Pet pet, CancellationToken cancellation = default)
        {
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync(cancellation);
        }
        public async Task UpdateAsync(Pet pet, CancellationToken cancellation = default)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync(cancellation);
        }
    }
}
