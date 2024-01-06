using Souq.Core.Entites;
using Souq.Core.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Repositorey.Repo
{
    public class BasketRepo : IBasketRepo
    {

        public BasketRepo(IConnectionMultiplexer Redis)
        {
            
        }
        public Task<bool> DeleteUpdateAsync(string Id)
        {
        
        }

        public Task<CustomerBasket> GetBasketAsync(string Id)
        {
         
        }

        public Task<CustomerBasket> UpdateBasketAsync(CustomerBasket Basket)
        {
            
        }
    }
}
