using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebApplication5.DTO;
using WebApplication5.Helpers;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Produces("application/json", "application/xml")]
    [EnableCors("AllowOrigin")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }


        [HttpPost("authenticate")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Authenticate([FromBody] UserLoginDto userLogin)
        {
            
            if (authenticationHelper.AuthenticatePrincipal(userLogin))
            {
                var tokenString = authenticationHelper.GenerateJwt(userLogin);
                return Ok(tokenString);
            }

            
            return Unauthorized();
        }
    }
}
