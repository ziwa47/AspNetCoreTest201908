using System.Threading.Tasks;
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
            string result;
            if (await _httpService.IsAuthAsync())
            {
                result = "IsAuth";
            }
            else
            {
                result = "IsNotAuth";
            }

            return Ok(new
            {
                IsAuth = result
            });
        }
    }
}