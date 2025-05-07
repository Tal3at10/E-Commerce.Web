using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface ICahceService
    {
        public Task SetCahceValueAsync(string key, object value, TimeSpan duration);

        public Task<string?> GetCacheValueAsync(string key);
    }
}
