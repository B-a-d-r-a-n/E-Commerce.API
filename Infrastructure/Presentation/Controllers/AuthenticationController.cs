
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Authentication;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager serviceManager) 
        : APIController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest request) 
            => Ok(await serviceManager.AuthenticationService.LoginAsync(request));
        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
            => Ok( await serviceManager.AuthenticationService.RegisterAsync(request));
           
        
    }
}
