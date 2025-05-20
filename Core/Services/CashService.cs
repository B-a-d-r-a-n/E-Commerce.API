﻿
using System.Text.Json;


namespace Services;
internal class CashService(ICashRepository cashRepository)
    : ICashService
{
    public async Task<string?> GetAsync(string cashKey)
        => await cashRepository.GetAsync(cashKey);

    public async Task SetAsync(string key, object value, TimeSpan expiration)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var serializedValue = JsonSerializer.Serialize(value, options);
        await cashRepository.SetAsync(key, serializedValue, expiration);
    }
}
