using Souq.Core.Entites;
using Souq.Core.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Souq.Repositorey.Repo
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase redis;

        public BasketRepo(IConnectionMultiplexer Redis)
        {
            redis = Redis.GetDatabase();
        }   
        public async Task<CustomerBasket> GetBasketAsync(string Id)
        {
         var Basket= await redis.StringGetAsync(Id);
            if (Basket.IsNull)
                return null;
            else
               return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<bool> DeleteUpdateAsync(string Id)
        {
            return await redis.KeyDeleteAsync(Id);
        }


        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket Basket)
        {
          var UpdatedOrUpdate=  await redis.StringSetAsync(Basket.Id, JsonSerializer.Serialize<CustomerBasket>(Basket), TimeSpan.FromDays(1));
            if(UpdatedOrUpdate==true)
            {
                return await GetBasketAsync(Basket.Id);
            }
            return null;
        }
    }
}
