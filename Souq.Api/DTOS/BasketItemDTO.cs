using System.ComponentModel.DataAnnotations;

namespace Souq.Api.DTOS
{
    public class BasketItemDTO
    {
        [Required ]
        public int Id { get; set; }
        [Required]

        public string ProductName { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1,1000,ErrorMessage ="Enter Valied Price . . . .  . . .")]

        public decimal Price { get; set; }
        [Required]
        [Range(1,10,ErrorMessage ="Must Enter Value Between 1 to 10") ]
        public int Quantity { get; set; }
        [Required]


        public string Brand { get; set; }
        [Required]

        public string Type { get; set; }
    }
}