using System.Linq.Expressions;
using Domain.Common;

namespace Application.Abstractions
{
    public interface IRepository<E> where E : Entity
    {
        Expression<Func<E, E>> GetSelector(Expression<Func<E, E>>? selector = null);
        Task<E?> GetByIdAsync(Guid id,
            Expression<Func<E, E>>? selector = null,
            CancellationToken cancellationToken = default);
        Task<ICollection<E>> GetAsync(Expression<Func<E, bool>> predicate,
            CancellationToken cancellationToken = default);
        Task<ICollection<E>> GetAllAsync(Expression<Func<E, E>>? selector = null,
            CancellationToken cancellationToken = default);
        Task CreateAsync(E entity,
            CancellationToken cancellation = default);
        void Update(E entity);
        void Delete(E entity);
    }
}
