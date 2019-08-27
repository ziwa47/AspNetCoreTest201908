using AspNetCoreTest201908.Model;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTest201908.Api.Lab00
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class Lab00Controller : Controller
    {
        public IActionResult Index()
        {
            return Ok(new AuthResult { IsAuth = true });
        }
        public ActionResult<AuthResult> Index2()
        {
            return new AuthResult { IsAuth = true };
        }
    }
}