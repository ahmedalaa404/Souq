using Souq.Core.Entites.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Services
{
    public interface IOrderServices
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId,Address ShippingAddress);



        //Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail);

        //Task<Order> GetOrderByIdForUserAsync(int orderId, string BuyerEmail); // Get OrderFor User When Take OrderID,BuyeEmail

    }
}
