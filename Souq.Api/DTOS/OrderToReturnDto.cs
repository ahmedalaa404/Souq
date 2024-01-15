using Souq.Core.Entites.Order_Aggregate;

namespace Souq.Api.DTOS
{
    public class OrderToReturnDto
    {

        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; } // Value Have Return From Order




        public Address ShippingAddress { get; set; } // One To One   Total of the Two Dimations



        public string  DeliveryMethod { get; set; }
        public decimal Coast { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;


        public decimal Total { get; set; }


        //public DeliveryMethod DeliveryMethod { get; set; }
        //[NotMapped] // Don`t Mapp To Column In DataBase
        //public decimal Total => SubTotal + DeliveryMethod.Cost;  //drevied Attribute - SubTotal+DeliveryCoast


    }
}
