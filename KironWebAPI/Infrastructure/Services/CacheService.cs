using KironWebAPI.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace KironWebAPI.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IMemoryCache cache, ILogger<CacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public T GetData<T>(string key)
        {
            var result = _cache.Get(key);
            if (result != null)
            {
                return (T)result;
            }
            return default;
        }

        public void RemoveData(string cachekey)
        {
            _cache.Remove(cachekey);
            _logger.Log(LogLevel.Information, "cleared cache");
        }

        public bool SetData<T>(string cachekey, T value, int time)
        {
            if (_cache.TryGetValue(cachekey, out var coinStats))
            {
                _logger.Log(LogLevel.Information, "data found in the cache");
                return false;
            }
            else
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromHours(45))
                   .SetAbsoluteExpiration(TimeSpan.FromHours(time))
                   .SetPriority(CacheItemPriority.Normal);

                _cache.Set(cachekey, value, cacheEntryOptions);
            }
            return true;
        }
    }
}
