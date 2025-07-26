namespace Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected readonly List<IDomainEvent> _domainEvents = [];

        protected Entity(Guid Id)
        {
            this.Id = Id;
        }

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
