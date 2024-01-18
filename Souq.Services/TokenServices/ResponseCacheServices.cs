using Souq.Core.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Souq.Services.TokenServices
{
    public class ResponseCacheServices : IResponseCacheServices
    {
        private readonly IDatabase Redis;

        public ResponseCacheServices(IConnectionMultiplexer redis)
        {
           Redis = redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string CacheKey, object Response, TimeSpan TimeToLive)
        {
            if (Response is null)
                return;

            var Options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var Data = JsonSerializer.Serialize(Response, Options);

          await  Redis.StringSetAsync(CacheKey, Data, TimeToLive);// PascalCase that Must Convert To CamalCase
        }

        public async Task<string> GetCachedResponseAsync(string CacheKey)
        {
            var CachrResponse=await  Redis.StringGetAsync(CacheKey);
            if (CachrResponse.IsNullOrEmpty) return null;
            return CachrResponse.ToString();
        }
    }
}
