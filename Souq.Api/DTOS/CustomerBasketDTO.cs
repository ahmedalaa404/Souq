using Souq.Core.Entites;
using System.ComponentModel.DataAnnotations;

namespace Souq.Api.DTOS
{
    public class CustomerBasketDTO
    {

        [Required]
        public string Id { get; set; }



        public List<BasketItemDTO> Items { get; set; }
        public string PaymentId { get; set; }

        public string ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }

    }
}
