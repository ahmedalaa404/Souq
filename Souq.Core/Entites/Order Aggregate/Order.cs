using Souq.Core.DataBase;using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations.Schema;using System.Linq;using System.Text;using System.Threading.Tasks;namespace Souq.Core.Entites.Order_Aggregate{    public class Order:BaseEntity    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, decimal subTotal, ICollection<OrderItem> items)
        {
            BuyerEmail = buyerEmail;


            ShippingAddress = shippingAddress;

            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            Items = items;
        }

        public string BuyerEmail { get; set; }        public DateTimeOffset  OrderDate { get; set; }=DateTimeOffset.Now;        public OrderStatus Status { get; set; } = OrderStatus.Pending;        public Address ShippingAddress { get; set; } // One To One   Total of the Two Dimations
      

        //[not]
        public int? DeliveryMethodId { get; set; }        public DeliveryMethod DeliveryMethod { get; set; }


        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();        public decimal   SubTotal    { get; set; }


        //[NotMapped] // Don`t Mapp To Column In DataBase        //public decimal Total => SubTotal + DeliveryMethod.Cost;  //drevied Attribute - SubTotal+DeliveryCoast        #region Dervied Attribute        public decimal GetTotal()        {            return SubTotal + DeliveryMethod.Cost;        }        #endregion        public string PaymentIntentId { get; set; } = string.Empty;    }}