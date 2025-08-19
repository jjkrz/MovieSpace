using Application.Abstractions;
using Domain.Movies;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Genre genre, CancellationToken cancellationToken = default)
        {
            await _context.Genres.AddAsync(genre, cancellationToken);
        }

        public async Task<Genre?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Id == Id, cancellationToken);
        }

        public void Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
        }

        public async Task<bool> ExistsWithName(string name, CancellationToken cancellationToken = default)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == name, cancellationToken);

            return genre != null;
        }

        public async Task<ICollection<Genre>> GetPaginatedAsync(int page, int size, string? search = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Genres
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m => m.Name.Contains(search));
            }

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Genres.CountAsync();
        }
    }
}
