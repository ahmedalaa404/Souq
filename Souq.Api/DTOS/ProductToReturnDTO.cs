using Souq.Core.DataBase;

namespace Souq.Api.DTOS
{
    public class ProductToReturnDTO
    {
        public int Id { get; set; }
        public string Name;
        public string Description;
        public string PictureUrl { get; set; } // Name Picture


        public decimal Price { get; set; }


        public int ProductBrandId { get; set; } //Not Null -- Is Cascad
        public string  ProductBrand { get; set; } // navigation property =One 



        public int ProductTypeId { get; set; } //Not Null -- Is Cascad
        public string ProductType { get; set; } //Navigation Property is one 



    }
}
