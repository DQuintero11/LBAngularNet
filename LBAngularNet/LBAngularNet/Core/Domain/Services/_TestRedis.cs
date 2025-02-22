using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace LBAngularNet.Core.Domain.Services
{
    public class _TestRedis
    {
        private readonly IDistributedCache _cache;

        public _TestRedis(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetCacheAsync<T>(string key, T value, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration };
            var json = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, json, options);
        }

        public async Task<T?> GetCacheAsync<T>(string key)
        {
            var json = await _cache.GetStringAsync(key);
            return json == null ? default : JsonSerializer.Deserialize<T>(json);
        }
    }
}
