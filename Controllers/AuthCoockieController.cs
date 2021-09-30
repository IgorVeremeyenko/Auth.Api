using Auth.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthCoockieController : ControllerBase
    {

        private readonly IAuthService authService;

        public AuthCoockieController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginModel model)
        {
            var user = authService.IsAuth(model);
            if (user is null)
            {
                return Unauthorized();
            }


            var principal = authService.CreatePrincipal(user, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                null);
            return Ok();
        }

       
        
    }
}
