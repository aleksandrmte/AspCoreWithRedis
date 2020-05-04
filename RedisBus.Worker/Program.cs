using System;
using Microsoft.Extensions.DependencyInjection;
using RedisBus.Common;
using RedisBus.Core;
using RedisBus.Worker.IntegrationEvent;

namespace RedisBus.Worker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices.Configure();
            var redisBus = serviceProvider.GetRequiredService<IRedisBus>();
            redisBus.SubscribeAsync<RedisIntegrationEventHandler, RedisIntegrationEvent>(nameof(RedisIntegrationEvent));
            Console.ReadKey();
        }
    }
}
