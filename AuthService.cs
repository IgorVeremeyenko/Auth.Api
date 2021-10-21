using Auth.Api.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api
{
    public class AuthService : IAuthService
    {
        public UserModel Authenticate(LoginModel model)
        {
            return model.Username == "admin" && model.Password == "admin"
                ? new UserModel
                {
                    Id = 15,
                    FirstName = "Igor",
                    LastName = "Veremeyenko"
                }
                : null;
        }

        public List<Claim> CreateClaims(UserModel model)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, JsonConvert.SerializeObject(model.Id.ToString())),
                new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(model.FullName)),
                new Claim("permissions", nameof(Permission.AccessExtended))
            };
        }

        public ClaimsPrincipal CreatePrincipal(UserModel model, string authScheme)
        {
            var claims = CreateClaims(model);

            var identity = new ClaimsIdentity(claims, authScheme);

            return new ClaimsPrincipal(identity);
        }

        public string CreateJwt(List<Claim> claims, string signingKey)
        {
            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                notBefore: now,
                expires: now.AddDays(365),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(signingKey)),
                    SecurityAlgorithms.HmacSha256)
                );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
