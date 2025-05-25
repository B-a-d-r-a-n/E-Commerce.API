
using System.ComponentModel.DataAnnotations;

namespace Shared.Authentication
{
    public record RegisterRequest([EmailAddress]string Email, string Password,string DisplayName,
        string UserName="n",string PhoneNumber="");

}
