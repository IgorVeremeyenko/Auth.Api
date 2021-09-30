using Auth.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Api
{
    public class AuthService : IAuthService
    {
        public UserModel IsAuth(LoginModel model)
        {
            return model.Username == "admin" && model.Password == "admin"
                ? new UserModel
                {
                    Id = 15,
                    FirstName = "Vasya",
                    LastName = "Doe"
                }
                : null;
        }

        public ClaimsPrincipal CreatePrincipal(UserModel model, string authScheme)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var identity = new ClaimsIdentity(claims, authScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}
