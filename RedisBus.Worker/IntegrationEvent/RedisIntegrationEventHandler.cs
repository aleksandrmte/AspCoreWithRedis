using RedisBus.Common;
using RedisBus.Core.Common;
using System;
using System.Threading.Tasks;

namespace RedisBus.Worker.IntegrationEvent
{
    public class RedisIntegrationEventHandler: IIntegrationEventHandler<RedisIntegrationEvent>
    {
        public Task HandleAsync(RedisIntegrationEvent @event)
        {
            Console.WriteLine($"{@event.Title} - {@event.Text}");
            return Task.CompletedTask;
        }
    }
}
