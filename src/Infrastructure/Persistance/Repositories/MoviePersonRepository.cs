using Application.Abstractions;
using Domain.MoviePeople;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class MoviePersonRepository : IMoviePersonRepository
    {
        private readonly ApplicationDbContext _context;

        public MoviePersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(MoviePerson person, CancellationToken cancellationToken = default)
        {
            await _context.MoviePeople.AddAsync(person, cancellationToken);
        }

        public void Delete(MoviePerson person)
        {
            _context.MoviePeople.Remove(person);
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.MoviePeople.CountAsync(cancellationToken);
        }

        public async Task<MoviePerson?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.MoviePeople.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<ICollection<MoviePerson>> GetPaginatedAsync(int page, int size, string? search = null, CancellationToken cancellationToken = default)
        {
            var query = _context.MoviePeople.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => (p.FirstName + " " + p.LastName).Contains(search));
            }

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);
        }

        public async Task<ICollection<MoviePerson>> GetByIdsAsync(ICollection<Guid> ids, CancellationToken cancellationToken = default)
        {
            var result = await _context.MoviePeople.Where(mp => ids.Contains(mp.Id) ).ToListAsync(cancellationToken);

            return result;
        }
    }
}

