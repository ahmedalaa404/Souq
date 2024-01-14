using Souq.Core.Entites.Order_Aggregate;
using System.ComponentModel.DataAnnotations;

namespace Souq.Api.DTOS
{
    public class OrderDto
    {

        [Required]
        public string BasketId { get; set; }
        [Required]

        public int DeliveryMethodId { get; set; }

        [Required]

        public AddressDto ShippingAddress  { get; set; }



    }
}
