using System;

namespace RedisBus.Core.Common
{
    public interface IIntegrationEvent
    {
        DateTime Created { get; set; }
    }
}
