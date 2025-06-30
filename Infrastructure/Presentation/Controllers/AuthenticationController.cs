
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail([EmailAddress]string email)
        {
            var result = await serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthenticationService.GetUserAddressAsync(email);
            return Ok(result);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthenticationService.UpdateUserAddressAsync(addressDTO,email!);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
       
            return Ok(await serviceManager.AuthenticationService.GetUserByEmail(email));
        }
    }
}
