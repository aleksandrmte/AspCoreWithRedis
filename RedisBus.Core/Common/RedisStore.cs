using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;

namespace RedisBus.Core.Common
{
    internal class RedisStore
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public RedisStore(IConfiguration configuration)
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { configuration["Redis:Connection"] }
            };

            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public ConnectionMultiplexer Connection => _lazyConnection.Value;

        public IDatabase RedisCache => Connection.GetDatabase();
    }
}
