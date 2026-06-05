
using Application.Abstractions;
using Domain.Movies;
using Domain.Sessions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _context;
        public SessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Session?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Sessions.FirstOrDefaultAsync(g => g.Id == Id, cancellationToken);
        }

        public async Task CreateAsync(Session session, CancellationToken cancellationToken = default)
        {
            await _context.Sessions.AddAsync(session, cancellationToken);
        }
    }
}
