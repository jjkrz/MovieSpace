using Application.Abstractions;
using Domain.Movies;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class MovieRepository: IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<List<Movie>> GetMoviesAsync(
                    int page,
                    int size,
                    string? search = null,
                    CancellationToken cancellationToken = default)
        {
            var query = _context.Movies
                .Include(m => m.Genres)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m => m.Title.Contains(search));
            }

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(Movie movie, CancellationToken cancellationToken)
        {
            await _context.Movies.AddAsync(movie, cancellationToken);
        }

        public async Task<Movie?> GetByIdWithRatingAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Movies.Include(m => m.Ratings).FirstOrDefaultAsync(m => m.Id == Id, cancellationToken);
        }

        public async Task<long> GetMoviesCountAsync(string? search = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m => m.Title.Contains(search));
            }

            return await query.LongCountAsync(cancellationToken);
        }

        public async Task<Movie?> GetByIdWithGenres(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Movies
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == Id, cancellationToken);
        }
    }
}
