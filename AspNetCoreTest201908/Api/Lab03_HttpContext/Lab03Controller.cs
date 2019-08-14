using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTest201908.Api.Lab03_HttpContext
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class Lab03Controller : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Lab03Controller(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index1()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok(new { IsAuth = true });
            }

            return Unauthorized();
        }

        public IActionResult Index2()
        {
            var email = GetClaim(ClaimTypes.Email);
            var myType = GetClaim("MyType");

            return Ok(new
            {
                Email = email,
                MyType = myType
            });
        }

        public IActionResult Index3()
        {
            var user = _httpContextAccessor.HttpContext.Session.GetString("user");
            return Ok(new { User = user });
        }

        public IActionResult Index4()
        {
            var user = _httpContextAccessor.HttpContext.Request.Cookies["user"];
            return Ok(new { User = user });
        }

        private string GetClaim(string type)
        {
            // return _httpContextAccessor.HttpContext.User.FindFirstValue(type);
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == type)?.Value;
        }
    }
}