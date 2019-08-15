using AspNetCoreTest201908.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreTest201908.Api.Lab02_IConfiguration
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class Lab022Controller : Controller
    {
        private readonly ServerHost _serverHost;

        public Lab022Controller(IOptions<ServerHost> options)
        {
            _serverHost = options.Value;
        }

        public IActionResult Index1()
        {
            var host = _serverHost.Host;
            return Ok(new ServerResult { Host = host });
        }
    }
}