using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueTask_Vs_Task_Sample
{
    public class Repository
    {
        //Implement IMemoryCache in your Create Request to Assure application.
        //Similarly there are some lookups (like contacts, emails) from SF whose value is retrieving frequently. In this case, we can use Redis Cache. 
        IMemoryCache _taskCache = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// For "Task" , getdata
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public async Task<string> GetDatawithTaskAsync(string cacheKey)
        {
            var value = _taskCache.Get<string>(cacheKey);
            if(value is null)
            {
                value = await GetData();
                _taskCache.Set(cacheKey, value,TimeSpan.FromHours(1));
            }
            return value;
        }

        /// <summary>
        /// For ValueTask, getData
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public async ValueTask<string> GetDatawithValueTaskAsync(string cacheKey)
        {
            var value = _taskCache.Get<string>(cacheKey);
            if(value is null)
            {
                value = await GetData();
                _taskCache.Set(cacheKey, value, TimeSpan.FromHours(1));
            }
            return value;
        }

        public async Task<string> GetData()
        {
            await Task.Delay(1000);
            return "Data From external service";
        }

    }
}
