using Application.Abstractions;
using Domain.Common;
using Domain.Movies;
using Infrastructure.Database;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class MovieRepository: IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task CreateAsync(Movie movie, CancellationToken cancellationToken)
        {
            await _context.Movies.AddAsync(movie, cancellationToken);
        }

        public async Task<Movie?> GetByIdWithRatingAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Movies.Include(m => m.Ratings).FirstOrDefaultAsync(m => m.Id == Id, cancellationToken);
        }
    }
}
