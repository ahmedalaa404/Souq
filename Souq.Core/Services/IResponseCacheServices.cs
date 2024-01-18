using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Services
{
    public interface IResponseCacheServices
    {


        Task CacheResponseAsync(string CacheKey,object Response,TimeSpan TimeToLive);
        Task<string> GetCachedResponseAsync(string CacheKey);
    }
}
