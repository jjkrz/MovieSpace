using Application.Abstractions;
using Domain.Movies;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class ProductionCountryRepository : IProductionCountryRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductionCountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ProductionCountry country, CancellationToken cancellationToken = default)
        {
            await _context.ProductionCountries.AddAsync(country, cancellationToken);
        }

        public void Delete(ProductionCountry country)
        {
            _context.ProductionCountries.Remove(country);
        }

        public async Task<bool> ExistsWithName(string name, CancellationToken cancellationToken = default)
        {
            var entity = await _context.ProductionCountries.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);

            return entity != null;
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ProductionCountries.CountAsync(cancellationToken);
        }

        public async Task<ProductionCountry?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await _context.ProductionCountries.FirstOrDefaultAsync(c => c.Id == Id, cancellationToken);
        }

        public async Task<ICollection<ProductionCountry>> GetPaginatedAsync(int page, int size, string? search = null, CancellationToken cancellationToken = default)
        {
            var query = _context.ProductionCountries
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.Contains(search));
            }

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);
        }
    }
}


