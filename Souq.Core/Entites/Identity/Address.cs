using System.ComponentModel.DataAnnotations.Schema;

namespace Souq.Core.Entites.Identity
{
    public class Address
    {
        public int Id { get; set; }

        public string FName { get; set; }

        public string Lname { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }


        [ForeignKey("User")]
        public string AppUserId { get; set; } //FK User
        public AppUser User { get; set; }


    }
}