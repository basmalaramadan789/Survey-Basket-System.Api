
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace SurveyBasket.Api.Services
{
    public class CacheService(IDistributedCache distributedCache) : ICacheService
    {
        private readonly IDistributedCache _distributedCache = distributedCache;

        public async Task<T?> GetAsunc<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            //insure if there cache with this key
            var cachedValue =await _distributedCache.GetStringAsync(key, cancellationToken);
            return cachedValue is null ?null :JsonSerializer.Deserialize<T>(cachedValue);// Deserilization
        }

        public async Task SetAsunc<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value),cancellationToken);//serelization
        }

        public async Task RemoveAsunc(string key, CancellationToken cancellationToken = default) 
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }

       
    }
}
