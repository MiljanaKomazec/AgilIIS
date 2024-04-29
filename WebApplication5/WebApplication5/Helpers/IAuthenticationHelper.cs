using System.Security.Principal;
using WebApplication5.DTO;

namespace WebApplication5.Helpers
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(UserLoginDto userLogin);

        public string GenerateJwt(UserLoginDto userLogin);
    }
}
