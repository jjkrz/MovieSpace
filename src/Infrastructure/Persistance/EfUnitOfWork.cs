using Application.Abstractions;
using Domain.Common;
using Infrastructure.Database;
using MediatR;

namespace Infrastructure.Persistance
{
    public sealed class EfUnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _ct;
        private readonly IPublisher _publisher;

        public EfUnitOfWork(ApplicationDbContext ct, IPublisher publisher)
        {
            _ct = ct;
            _publisher = publisher;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _ct.SaveChangesAsync(cancellationToken);

            await PublishDomainEvents();

            return result;
        }

        private async Task PublishDomainEvents()
        {
            var domainEvents = _ct.ChangeTracker
               .Entries<Entity>()
               .Select(entry => entry.Entity)
               .SelectMany(entity =>
               {
                   List<IDomainEvent> domainEvents = entity.PopDomainEvents();

                   return domainEvents;
               })
               .ToList();

            foreach (IDomainEvent domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}
