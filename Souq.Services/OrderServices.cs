using Souq.Core.DataBase;
using Souq.Core.Entites.Order_Aggregate;
using Souq.Core.Repositories;
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
        private readonly IBasketRepo _basketRepo;
        private readonly IGenericRepository<Product> ProductRepo;
        private readonly IGenericRepository<Order> _OrdersRepo;

        public IGenericRepository<DeliveryMethod> _DeliveryMethodeRepo { get; }

        public OrderServices(IBasketRepo BasketRepo,IGenericRepository<Product> productRepo,IGenericRepository<DeliveryMethod> DeliveryMethodeRepo,IGenericRepository<Order> ordersRepo)
        {
            _basketRepo = BasketRepo;
            ProductRepo = productRepo;
            _DeliveryMethodeRepo = DeliveryMethodeRepo;
            _OrdersRepo = ordersRepo;
        }


        public async  Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            var Basket = await _basketRepo.GetBasketAsync(BasketId); // Get Basket From Basket Repo

            // Get Select Items 

            var Orderitems= new List<OrderItem>();
            if(Basket?.Items?.Count>0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await ProductRepo.GetByIdAsync(item.Id);
                    var ProductItemOrder = new ProductItemOrder()
                    {
                        ProductName = product.Name,
                        ProductId = item.Id,
                        ProductPictureUrl = product.PictureUrl,
                    };

                    var OrderItem = new OrderItem(ProductItemOrder, product.Price , item.Quantity);

                    Orderitems.Add(OrderItem);

                    _OrdersRepo.
                }


                //Subtotal

                var SubTotal= Orderitems.Sum(x=>x.Price*x.Quantity);
                // Delivery Methode
                var Delivery = await _DeliveryMethodeRepo.GetByIdAsync(DeliveryMethodId);


                var order=new Order(BuyerEmail,ShippingAddress, Delivery, SubTotal, Orderitems);




            }



        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string BuyerEmail)
        {
           
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
           
        }
    }
}
