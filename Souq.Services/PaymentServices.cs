using Microsoft.Extensions.Configuration;
using Entites = Souq.Core.DataBase;
using Souq.Core.Entites;
using Souq.Core.Entites.Order_Aggregate;
using Souq.Core.Repositories;
using Souq.Core.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Souq.Core.Specification;

namespace Souq.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IConfiguration Config;
        private readonly IBasketRepo BasketRepo;
        private readonly IUniteOFWork UniteOFWork;

        public PaymentServices(IConfiguration config, IBasketRepo basketRepo, IUniteOFWork UniteOFWork)
        {
            Config = config;
            BasketRepo = basketRepo;
            this.UniteOFWork = UniteOFWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = Config["StripSettings:Secretkey"]; //U Have Api Key
            var basket = await BasketRepo.GetBasketAsync(BasketId);
            if (basket is null)
                return null;


            var ShippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await UniteOFWork.Repositorey<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                ShippingPrice = DeliveryMethod.Cost;
                basket.ShippingPrice = DeliveryMethod.Cost;
            }
            if (basket?.Items.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var Product = await UniteOFWork.Repositorey<Entites.Product>().GetByIdAsync(item.Id);
                    if(item.Price !=Product.Price)
                    {
                        item.Price = Product.Price;
                    }


                    PaymentIntent PaymentIntent;
                    var Services = new PaymentIntentService();
                    if(string.IsNullOrEmpty(basket.PaymentId))// Create In The Firest
                    {
                        var Options = new PaymentIntentCreateOptions()
                        {
                            Amount = (long)basket.Items.Sum(x => x.Price * item.Quantity + ShippingPrice) * 100,// Cent;
                            Currency = "usd",
                            PaymentMethodTypes = new List<string>() { "card" }


                        };

                        PaymentIntent= await Services.CreateAsync(Options);
                        basket.PaymentId = PaymentIntent.Id;
                        basket.ClientSecret = PaymentIntent.ClientSecret;

                    }
                    else //Update
                    {

                        var Options = new PaymentIntentUpdateOptions()
                        {
                            Amount = (long)basket.Items.Sum(x => x.Price * item.Quantity + ShippingPrice) * 100,// Cent;

                        };
                       await Services.UpdateAsync(basket.PaymentId,Options);

                    }


                }


            }
            await BasketRepo.UpdateBasketAsync(basket);

            return basket;
        }

        public async Task<Order> UpdatePaymentIntentToSucceededOrFailed(string id, bool StatusSuccesOrNot)
        {
            var Sepc = new OrderWithPaymentIntentIdSpec(id);

            var Order = await UniteOFWork.Repositorey<Order>().GetByIdAsyncWithSpec(Sepc);


            if(StatusSuccesOrNot)
            {
                Order.Status = OrderStatus.PaymentReceived;

            }
            else
            {
                Order.Status = OrderStatus.PaymentFailed;
            }

            UniteOFWork.Repositorey<Order>().Update(Order);
            UniteOFWork.Complete();



            return Order;
        }
    }
}
