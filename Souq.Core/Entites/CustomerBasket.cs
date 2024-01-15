using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Entites
{
    public  class CustomerBasket
    {

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }

        public CustomerBasket()
        {
            Id= Guid.NewGuid().ToString();
        }





        public CustomerBasket(string Id )
        {
           this.Id = Id;
        }




        public string PaymentId { get; set; }

        public string ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
