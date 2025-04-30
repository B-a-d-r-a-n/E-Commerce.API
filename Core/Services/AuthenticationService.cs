


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
    internal class AuthenticationService(UserManager<ApplicationUser> userManager)
        : IAuthenticationService
    {
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            //check if there is user with email address
            var user = await userManager.FindByEmailAsync(request.Email)
                ?? throw new UserNotFoundException(request.Email);
            // check password for this user
            var isValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (isValid) return new(request.Email, user.DisplayName, await CreateTokenAsync(user));
            
                
            
            // return response with token
            throw new UnauthorizedAException();
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                DisplayName = request.DisplayName


            };

            var result = await userManager.CreateAsync(user, request.Password);
            if(result.Succeeded) return new(request.Email, user.DisplayName, await CreateTokenAsync(user));
           var errors = result.Errors.Select(e=> e.Description).ToList();
            throw new BadRequestException(errors);
        }
        private  async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Email,user.Email!),
                new(ClaimTypes.Name,user.UserName!),
               
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            string secretkey = "UITjyCclF5Rpu/ZctfN0YynnfLdc0gdEQrU9Js6wEJ0=";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "myissuer",
                audience: "myaudience",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }
    }
}
