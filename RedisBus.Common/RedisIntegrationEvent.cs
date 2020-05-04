using System;
using System.Collections.Generic;
using System.Text;
using RedisBus.Core.Common;

namespace RedisBus.Common
{
    public class RedisIntegrationEvent : IIntegrationEvent
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
