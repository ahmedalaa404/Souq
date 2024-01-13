using Souq.Core.Entites.Order_Aggregate;
using Souq.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Services
{
    public class OrderServices : IOrderServices
    {
        public Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
           
        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string BuyerEmail)
        {
           
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
           
        }
    }
}
