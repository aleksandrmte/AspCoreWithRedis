using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedisBus.Core.Common;

namespace RedisBus.Core
{
    public interface IRedisBus
    {
        Task PublishAsync(string channelName, IIntegrationEvent @event);

        Task SubscribeAsync<THandler, TEvent>(string channelName) where THandler : IIntegrationEventHandler<TEvent>
            where TEvent : IIntegrationEvent;
    }
}
