using System.Linq.Expressions;
using Application.Abstractions;
using Domain.Common;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class Repository<E>(ApplicationDbContext context) : IRepository<E> where E : Entity, new()
    {
        public async Task CreateAsync(E entity, CancellationToken cancellation = default)
        {
            await context.Set<E>().AddAsync(entity, cancellation);
        }
        public void Delete(E entity)
        {
            context.Set<E>().Remove(entity);
        }
        public async Task<ICollection<E>> GetAsync(Expression<Func<E, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Set<E>()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<ICollection<E>> GetAllAsync(Expression<Func<E, E>>? selector = null, CancellationToken cancellationToken = default)
        {
            return await context.Set<E>()
                .Select(GetSelector(selector))
                .ToListAsync(cancellationToken);
        }

        public async Task<E?> GetByIdAsync(Guid id, Expression<Func<E, E>>? selector = null, CancellationToken cancellationToken = default)
        {
            return await context.Set<E>()
                .Where(e => e.Id == id)
                .Select(GetSelector(selector))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Expression<Func<E, E>> GetSelector(Expression<Func<E, E>>? selector = null)
        {
            return selector ?? (e => e);
        }

        public void Update(E entity)
        {
            context.Set<E>().Update(entity);
        }
    }
}
