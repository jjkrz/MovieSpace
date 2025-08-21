using Application.Abstractions;
using Domain.MoviePersonality;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class MovieRoleRepository : IMovieRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(MovieRole movieRole, CancellationToken cancellationToken = default)
        {
            await _context.MovieRoles.AddAsync(movieRole, cancellationToken);
        }
        public async Task<bool> ExistsWithName(string RoleName, CancellationToken cancellationToken = default)
        {
            var genre = await _context.MovieRoles.FirstOrDefaultAsync(m => m.RoleName == RoleName, cancellationToken);

            return genre != null;
        }

        public async Task<MovieRole?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.MovieRoles.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Delete(MovieRole role)
        {
            _context.MovieRoles.Remove(role);
        }

        public async Task<ICollection<MovieRole>> GetPaginatedAsync(int page, int size, string? search = null, CancellationToken cancellationToken = default)
        {
            var query = _context.MovieRoles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(r => r.RoleName.Contains(search));
            }

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.MovieRoles.CountAsync(cancellationToken);
        }
    }
}
