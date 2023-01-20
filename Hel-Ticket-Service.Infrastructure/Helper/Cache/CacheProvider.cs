using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using Hel_Ticket_Service.Domain;

namespace Hel_Ticket_Service.Infrastructure;
    
    public class CacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _cache;

        public CacheProvider(IDistributedCache cache)
        {
            _cache = cache;
            
        }
        public async Task<T?> GetFromCache<T>(string reference) where T : class
        {
            Log.Information("Getting from cache...");
            var cache = new Cache($"{nameof(T)}_{reference}_{DateTime.Now:yyyyMMdd_hhmm}");
            var cachedData = await _cache.GetStringAsync(cache.Key);
            return cachedData == null ? null : JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetToCache<T>(string reference,T value,
        TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null) where T : class
        {
            Log.Information("Setting in cache...");
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = slidingExpireTime;
            var data = JsonSerializer.Serialize(value);
            var cache = new Cache($"{nameof(T)}_{reference}_{DateTime.Now:yyyyMMdd_hhmm}");
            await _cache.SetStringAsync(cache.Key, data , options);
            Log.Information("Cache set completed...");
        }

        public async Task ClearCache<T>(string reference) where T : class
        {
            Log.Information("Clearing cache...");
            var cache = new Cache($"{nameof(T)}_{reference}_{DateTime.Now:yyyyMMdd_hhmm}");
            await _cache.RemoveAsync(cache.Key);
        }
    }
