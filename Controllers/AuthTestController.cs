using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Auth.Api.Models;


namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var userIdentity = HttpContext.User.Identity;
            if (userIdentity.IsAuthenticated)
            {
                return Ok(userIdentity.Name);
            }
            return Unauthorized();
        }

        // [HttpGet("extended")]
        // [RequirePermission("AccessExtended")]
        // public IActionResult GetExtended()
        // {
        //     return Ok();
        // }
    }
}
