using Souq.Core.DataBase;
using Souq.Core.Entites.Order_Aggregate;
using Souq.Core.Repositories;
using Souq.Core.Services;
using Souq.Core.Specification.OrderSpecification;
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
        private readonly IUniteOFWork uniteOFWork;

        ///private readonly IGenericRepository<Product> ProductRepo;
        ///private readonly IGenericRepository<Order> _OrdersRepo;
        ///public IGenericRepository<DeliveryMethod> _DeliveryMethodeRepo { get; }

        public OrderServices(IBasketRepo BasketRepo,IUniteOFWork  UniteOFWork   /*IGenericRepository<Product> productRepo,IGenericRepository<DeliveryMethod> DeliveryMethodeRepo,IGenericRepository<Order> ordersRepo*/)
        {
            _basketRepo = BasketRepo;
            uniteOFWork = UniteOFWork;
            ///ProductRepo = productRepo;
            ///_DeliveryMethodeRepo = DeliveryMethodeRepo;
            ///_OrdersRepo = ordersRepo;
        }    


        public async  Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            var Basket = await _basketRepo.GetBasketAsync(BasketId); // Get Basket From Basket Repo

            // Get Select Items 

            var Orderitems= new List<OrderItem>();
            if(Basket?.Items?.Count>0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await uniteOFWork.Repositorey<Product>().GetByIdAsync(item.Id);
                    var ProductItemOrder = new ProductItemOrder()
                    {
                        ProductName = product.Name,
                        ProductId = item.Id,
                        ProductPictureUrl = product.PictureUrl,
                    };

                    var OrderItem = new OrderItem(ProductItemOrder, product.Price , item.Quantity);

                    Orderitems.Add(OrderItem);

               
                }


                //Subtotal

                var SubTotal= Orderitems.Sum(x=>x.Price*x.Quantity);
                // Delivery Methode
                var Delivery = await uniteOFWork.Repositorey<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);


                var order=new Order(BuyerEmail,ShippingAddress, Delivery, SubTotal, Orderitems);


                await uniteOFWork.Repositorey<Order>().Add(order);
                var Resulte = await uniteOFWork.Complete();
                if (Resulte <= 0)
                    return null;
                return order;

            }
            return null;



        }

        public async  Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            var Spec = new OrderSpecification(BuyerEmail);
            var Orders = await uniteOFWork.Repositorey<Order>().GetAllAsycWithSpec(Spec);
            return Orders;


        }


        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string BuyerEmail)
        {
            var Spec = new OrderSpecification(orderId, BuyerEmail);
            var Orders = await uniteOFWork.Repositorey<Order>().GetByIdAsyncWithSpec(Spec);
            return Orders;
        }


    }
}
