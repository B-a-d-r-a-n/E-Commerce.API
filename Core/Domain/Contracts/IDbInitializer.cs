
using Domain.Models;
using System.Text.Json;

namespace Domain.Contracts
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
        Task InitializeIdentityAsync();
    }
}
