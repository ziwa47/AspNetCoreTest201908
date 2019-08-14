using System.Threading.Tasks;
using AspNetCoreTest201908.Model;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTest201908.Api.Lab05_Service
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class Lab05Controller : Controller
    {
        private readonly IHttpService _httpService;

        public Lab05Controller(IHttpService httpService)
        {
            _httpService = httpService;
        }

        [HttpGet]
        public async Task<IActionResult> Index1()
        {
            var result = await _httpService.IsAuthAsync();
            return Ok(new AuthResult { IsAuth = result });
        }
    }
}