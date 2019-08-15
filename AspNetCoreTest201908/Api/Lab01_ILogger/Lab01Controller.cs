using AspNetCoreTest201908.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreTest201908.Api.Lab01_ILogger
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class Lab01Controller : Controller
    {
        private readonly ILogger<Lab01Controller> _logger;

        public Lab01Controller(ILogger<Lab01Controller> logger)
        {
            _logger = logger;
        }

        public IActionResult Index1()
        {
            _logger.LogInformation("log information");

            return Ok(new AuthResult { IsAuth = true });
        }
    }
}