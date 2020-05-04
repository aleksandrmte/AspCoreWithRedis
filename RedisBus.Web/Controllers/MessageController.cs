using Microsoft.AspNetCore.Mvc;
using RedisBus.Common;
using RedisBus.Core;

namespace RedisBus.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IRedisBus _redisBus;

        public MessageController(IRedisBus redisBus)
        {
            _redisBus = redisBus;
        }
        
        [HttpPost]
        public async void Post([FromBody] RedisIntegrationEvent message)
        {
            await _redisBus.PublishAsync(nameof(RedisIntegrationEvent), message);
        }
    }
}
