using Application.Abstractions;
using Application.Common.Dto;
using Application.Common.ReadModels;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.ReadOnlyRepositories
{
    public class ReviewReadRepository : IReviewReadRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReviewReadModel>> GetRecentReviewsForMovieAsync(Guid movieId, int count)
        {
            return await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .OrderByDescending(r => r.CreatedAt)
                .Take(count)
                .Join(_context.Users,
                      r => r.UserId,
                      u => u.Id,
                      (r, u) => new ReviewReadModel
                      {
                          AuthorId = r.UserId,
                          AuthorName = u.UserName ?? "No-name",
                          Content = r.Content,
                          Rating = r.Rating,
                          CreatedAt = r.CreatedAt
                      })
                .ToListAsync();
        }
    }
}
