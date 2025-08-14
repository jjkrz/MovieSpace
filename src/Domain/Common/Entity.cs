namespace Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
        }

        protected readonly List<IDomainEvent> _domainEvents = [];

        public List<IDomainEvent> PopDomainEvents()
        {
            var domainEvents = _domainEvents.ToList();
            _domainEvents.Clear();

            return domainEvents;
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
