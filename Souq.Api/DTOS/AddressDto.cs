using System.ComponentModel.DataAnnotations;

namespace Souq.Api.DTOS
{
    public class AddressDto
    {


        //public int Id { get; set; }

        [Required]
        public string FName { get; set; }
        [Required]

        public string Lname { get; set; }
        [Required]

        public string Street { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string Country { get; set; }


    }
}
