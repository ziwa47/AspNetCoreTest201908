using AspNetCoreTest201908.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreTest201908.Api.Lab02_IConfiguration
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class Lab021Controller : Controller
    {
        private readonly IConfiguration _configuration;

        public Lab021Controller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index1()
        {
            var host = _configuration["Server:Host"];
            return Ok(new ServerResult { Host = host });
        }

        public IActionResult Index2()
        {
            var host = _configuration.GetValue<string>("Server:Host");
            return Ok(new ServerResult { Host = host });
        }
    }
}