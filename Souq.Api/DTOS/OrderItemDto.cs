namespace Souq.Api.DTOS
{
    public class OrderItemDto
    {





        public int Id { get; set; }
        public decimal Price { get; set; }// can U Have Deffirent here 

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string ProductPictureUrl { get; set; }
    }
}