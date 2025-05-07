using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Services.Abstraction;

namespace Services
{
    public class CacheService : ICahceService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            this._cacheRepository = cacheRepository;
        }
        public async Task<string?> GetCacheValueAsync(string key)
        {
            var value = await _cacheRepository.GetAsync(key);
            return value is null ? null : value;
        }

        public async Task SetCahceValueAsync(string key, object value, TimeSpan duration)
        {
           await _cacheRepository.SetAsync(key, value, duration);
        }
    }
}
