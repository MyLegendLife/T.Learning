using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace T.EventBus.Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public ValuesController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            
            await _eventBus.PublishAsync("UserRegister", "This message from EventBus");

            return "Publish successed from Eventbus";
        }

        [HttpPost]
        public async Task<string> Post([FromBody] string message)
        {
            return await Task.FromResult(message);
        }
    }
}
