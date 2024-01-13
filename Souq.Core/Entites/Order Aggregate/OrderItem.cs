using Souq.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Entites.Order_Aggregate
{
    public class OrderItem:BaseEntity // In Database // Product Have U Add In Order
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder Product { get; set; } //Not Have Change

        public decimal Price { get; set; }// can U Have Deffirent here 

        public int Quantity { get; set; }




    }
}
