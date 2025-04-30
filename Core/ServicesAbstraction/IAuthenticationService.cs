
using Shared.Authentication;

namespace ServicesAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest request);

        Task<UserResponse> RegisterAsync(RegisterRequest request);
    }
}
