using Souq.Core.Entites.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Specification.OrderSpecification
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification(string Email):base(x=>x.BuyerEmail==Email)
        {
            Includes.Add(x => x.DeliveryMethod);
            Includes.Add(x => x.Items);
            AddOrderByDesci(x => x.OrderDate);
        }



    }
}
