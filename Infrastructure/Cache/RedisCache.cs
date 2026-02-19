using Application.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Caching.Distributed;
namespace Infrastructure.Cache
{
    public class RedisCache : ICache
    {
        private readonly IDistributedCache _cache;

        public RedisCache(IDistributedCache cache) => _cache = cache;

        public async Task<string> GetOrCreateAsync(string key, Func<Task<string>> factory, CancellationToken ct = default)
        {
            var cached = await _cache.GetStringAsync(key, ct);
            if (cached != null) return cached;

            string newValue = await factory();
            await _cache.SetStringAsync(key, newValue, ct);
            return newValue;
        }
    }
}
