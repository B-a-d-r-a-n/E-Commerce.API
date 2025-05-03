
namespace Shared.Authentication
{
    public record RegisterRequest(string Email, string Password,string DisplayName,
        string UserName,string PhoneNumber);

}
