using Auth.Api.Models;
using System.Security.Claims;

namespace Auth.Api
{
    public interface IAuthService
    {
        ClaimsPrincipal CreatePrincipal(UserModel model, string authScheme);
        UserModel IsAuth(LoginModel model);
    }
}