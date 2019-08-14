using AspNetCoreTest201908.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AspNetCoreTest201908.Api.Lab01_IConfiguration
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class Lab01Controller : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ServerHost _serverHost;

        public Lab01Controller(IConfiguration configuration, IOptions<ServerHost> options)
        {
            _configuration = configuration;
            _serverHost = options.Value;
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

        public IActionResult Index3()
        {
            var host = _serverHost.Host;
            return Ok(new ServerResult { Host = host });
        }
    }
}