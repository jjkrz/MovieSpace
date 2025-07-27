namespace Domain.Common
{
    public interface IHasDomainEvents
    {
        List<IDomainEvent> PopDomainEvents();
    }
}
