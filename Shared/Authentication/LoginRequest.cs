
using System.ComponentModel.DataAnnotations;

namespace Shared.Authentication
{
    public record LoginRequest([EmailAddress]string Email, string Password);
   
}
