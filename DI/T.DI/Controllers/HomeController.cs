using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using T.DI.Services;

namespace T.DI.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        private readonly IOneService _oneService;
        public ITwoService _twoService { get; set; }


        public HomeController(IOneService oneService)
        {
            _oneService = oneService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var a = await _oneService.GetOne();
            var b = await _twoService.GetTwo();

            return a + b;
        }
    }
}
