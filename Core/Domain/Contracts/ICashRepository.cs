namespace Domain.Contracts;
public interface ICashRepository
{
    Task<string?> GetAsync(string cashKey);
    Task SetAsync(string cashKey, string value, TimeSpan expiration);
}
