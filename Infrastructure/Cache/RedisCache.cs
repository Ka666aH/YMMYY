using Application.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
namespace Infrastructure.Cache
{
    public class RedisCache : ICache
    {
        private readonly IDistributedCache _cache;

        public RedisCache(IDistributedCache cache) => _cache = cache;

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, CancellationToken ct = default)
        {
            var cached = await _cache.GetStringAsync(key, ct);
            if (cached != null) return JsonSerializer.Deserialize<T>(cached)!;

            var newValue = await factory();
            await _cache.SetStringAsync(key, JsonSerializer.Serialize(newValue), ct);
            return newValue;

            //var cached = await _cache.GetStringAsync(key, ct);
            //if (cached != null) return cached;

            //string newValue = await factory();
            //await _cache.SetStringAsync(key, newValue, ct);
            //return newValue;
        }
    }
}