
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Open.IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValueController : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("{action}")]
        public IActionResult AuthData()
        {
            return Ok("Auth");
        }

        [HttpGet]
        [Route("{action}")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminData()
        {
            return Ok("Admin");
        }

        [HttpGet]
        [Route("{action}")]
        [Authorize(Roles = "User")]
        public IActionResult UserData()
        {
            return Ok("User");
        }
    }
}
