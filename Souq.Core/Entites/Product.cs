using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.DataBase
{
    public class Product:BaseEntity
    {
        [Required]

        public string  Name { get; set; }

        public string  Description { get; set; }

        public string PictureUrl { get; set; } // Name Picture


        public decimal Price { get; set; }



        //[ForeignKey("ProductBrand")] // 
        public int ProductBrandId { get; set; } //Not Null -- Is Cascad
        public ProductBrand ProductBrand { get; set; } // navigation property =One 



        public int ProductTypeId { get; set; } //Not Null -- Is Cascad
        public ProductType ProductType { get; set; } //Navigation Property is one 

    }
}
