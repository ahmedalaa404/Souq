using System.ComponentModel.DataAnnotations.Schema;

namespace Souq.Core.Entites.Order_Aggregate
{
    [NotMapped]
    public class ProductItemOrder
    {
        public ProductItemOrder()
        {
            
        }
        public ProductItemOrder(int productId, string productName, string productPictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPictureUrl = productPictureUrl;
        }

        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string ProductPictureUrl { get; set; }






    }
}