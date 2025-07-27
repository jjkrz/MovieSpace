using Domain.Common;
using Domain.Movies;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    public sealed class User : IdentityUser<Guid>, IHasDomainEvents
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        public User(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }

        private readonly List<Rating> _ratings = [];

        public List<IDomainEvent> PopDomainEvents()
        {
            var domainEvents = _domainEvents.ToList();
            _domainEvents.Clear();

            return domainEvents;
        }

        private void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
