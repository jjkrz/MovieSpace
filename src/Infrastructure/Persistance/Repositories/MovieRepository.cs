using Application.Abstractions;
using Application.Services;
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
        
        public async Task<List<MovieRatingData>> GetAllMoviesRatingDataAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .OrderBy(m => m.Id)
                .Skip(skip)
                .Take(take)
                .Select(m => new MovieRatingData(
                    m.Id,
                    m.Ratings.Any() ? m.Ratings.Average(r => r.Score) : null,
                    m.Ratings.Count))
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateMovieRatingsAsync(List<MovieRatingUpdate> updates, CancellationToken cancellationToken = default)
        {
            foreach (var update in updates)
            {
                await _context.Movies
                    .Where(m => m.Id == update.MovieId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(m => m.AverageRating, update.AverageRating)
                        .SetProperty(m => m.RatingCount, update.RatingCount),
                    cancellationToken);
            }
        }

        public async Task<List<MovieRatingData>> GetMoviesWithNewRatingsAsync(DateTime since, CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .Where(m => m.Ratings.Any(r => r.CreatedAt >= since || r.UpdatedAt >= since))
                .Select(m => new MovieRatingData(
                    m.Id,
                    m.Ratings.Any() ? m.Ratings.Average(r => r.Score) : null,
                    m.Ratings.Count))
                .ToListAsync(cancellationToken);
        }

        public async Task<Movie?> GetByIdWithCast(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Movies
                .Include(m => m.MoviePeople)
                .FirstOrDefaultAsync(m => m.Id == Id, cancellationToken);
        }

        public async Task<Movie?> GetByIdWithReviewsAndRatingsByUserId(Guid Id, Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Movies
                .Include(m => m.Ratings)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == Id, cancellationToken);
        }

        public async Task<Movie?> GetByIdWithProductionCountriesAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Movies
                .Include(m => m.ProductionCountries)
                .FirstOrDefaultAsync(m => m.Id == Id, cancellationToken);
        }

        public async Task<Movie?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Movies
                .Include(m => m.MoviePeople)
                    .ThenInclude(mp => mp.MovieRole)
                .Include(m => m.MoviePeople)
                    .ThenInclude(mp => mp.MoviePerson)
                .Include(m => m.Genres)
                .Include(m => m.ProductionCountries)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == Id, cancellationToken);
        }
    }
}
