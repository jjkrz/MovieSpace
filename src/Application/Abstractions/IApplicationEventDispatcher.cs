using MediatR;

namespace Application.Abstractions
{
    public interface IApplicationEventDispatcher
    {
        Task PublishApplicationEvent(IApplicationEvent applicationEvent);
    }

    public class ApplicationEventDispatcher : IApplicationEventDispatcher
    {
        private readonly IPublisher _publisher;
        public ApplicationEventDispatcher(IPublisher publisher)
        {
            _publisher = publisher;
        }
        public async Task PublishApplicationEvent(IApplicationEvent applicationEvent)
        {
            await _publisher.Publish(applicationEvent);
        }
    }
}
