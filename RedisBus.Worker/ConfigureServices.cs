using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedisBus.Common;
using RedisBus.Core;
using RedisBus.Core.Common;
using RedisBus.Worker.IntegrationEvent;
using System.IO;

namespace RedisBus.Worker
{
    public class ConfigureServices
    {
        public static ServiceProvider Configure()
        {
            //setup our DI
            var services = new ServiceCollection()
                .AddSingleton<IRedisBus, Core.RedisBus>()
                .AddTransient<IIntegrationEventHandler<RedisIntegrationEvent>, RedisIntegrationEventHandler>();

            services.Add(new ServiceDescriptor(typeof(IConfiguration),
                provider => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json",
                        optional: false,
                        reloadOnChange: true)
                    .Build(),
                ServiceLifetime.Singleton));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
