
using Shared.Authentication;

namespace ServicesAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest request);

        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task<bool> CheckEmailAsync(string email);

        Task<AddressDTO> GetUserAddressAsync(string email);

        Task<AddressDTO> UpdateUserAddressAsync(AddressDTO address,string email);

        Task<UserResponse> GetUserByEmail(string email);


    }
}
