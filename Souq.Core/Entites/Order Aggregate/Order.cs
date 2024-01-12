using Souq.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Entites.Order_Aggregate
{
    public class Order:BaseEntity
    {

        public string BuyerEmail { get; set; }


        public DateTimeOffset  OrderDate { get; set; }=DateTimeOffset.Now;


        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; } // One To One   Total of the Two Dimations


        public DeliveryMethod DeliveryMethod { get; set; }


    }

}
