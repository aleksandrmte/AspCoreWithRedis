using System.Threading.Tasks;

namespace RedisBus.Core.Common
{
    public interface IIntegrationEventHandler<in TEvent> where TEvent : IIntegrationEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
