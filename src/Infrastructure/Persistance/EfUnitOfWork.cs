using Application.Abstractions;
using Application.Sessions.CreateSession;
using Domain.Common;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var result = await _ct.SaveChangesAsync(cancellationToken);

                await PublishDomainEvents();

                return result;
            }
            catch (DbUpdateException ex)
            {
                throw new DuplicateAccessCodeException();
            }
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
