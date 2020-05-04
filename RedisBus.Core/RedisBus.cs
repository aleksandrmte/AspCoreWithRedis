using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RedisBus.Core.Common;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedisBus.Core
{
    public class RedisBus: IRedisBus
    {
        private readonly IDatabase _redisDatabase;
        private readonly IServiceProvider _serviceProvider;

        public RedisBus(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var redisStore = new RedisStore(configuration);
            _redisDatabase = redisStore.RedisCache;
        }

        public async Task PublishAsync(string channelName, IIntegrationEvent @event)
        {
            var jsonMessage = JsonConvert.SerializeObject(@event);
            var pub = _redisDatabase.Multiplexer.GetSubscriber();
            await pub.PublishAsync(channelName, jsonMessage);
        }

        public async Task SubscribeAsync<THandler, TEvent>(string channelName) where THandler : IIntegrationEventHandler<TEvent> where TEvent : IIntegrationEvent
        {
            var sub = _redisDatabase.Multiplexer.GetSubscriber();
            await sub.SubscribeAsync(channelName,  async (channel, jsonMessage) => {
                await HandleMessage<TEvent>(jsonMessage);
            });
        }
        private async Task HandleMessage<TEvent>(string jsonMessage) where TEvent : IIntegrationEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IIntegrationEventHandler<TEvent>>();
            var message = JsonConvert.DeserializeObject<TEvent>(jsonMessage);
            await handler.HandleAsync(message);
        }
    }
}
