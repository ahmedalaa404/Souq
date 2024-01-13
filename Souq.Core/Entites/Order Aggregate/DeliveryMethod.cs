using Souq.Core.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Entites.Order_Aggregate
{
    public class DeliveryMethod : BaseEntity
    {

        public string ShortName { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string DeliveryTime { get; set; }

        ICollection<OrderItem> Items =new  HashSet<OrderItem>();


    }
}
