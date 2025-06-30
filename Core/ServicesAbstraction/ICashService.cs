namespace ServicesAbstraction;
public interface ICashService
{
    Task<string?> GetAsync(string cashKey);
    Task SetAsync(string key, object value, TimeSpan expiration);
}
