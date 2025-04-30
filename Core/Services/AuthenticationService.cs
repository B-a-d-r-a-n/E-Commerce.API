


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
        private static async Task<string> CreateTokenAsync(ApplicationUser user)
        {
           await  Task.Delay(TimeSpan.FromSeconds(3));
            return "JWTToken";

        }
    }
}
