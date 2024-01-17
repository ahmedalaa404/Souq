using Souq.Core.Entites.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Specification
{
    public class OrderWithPaymentIntentIdSpec:BaseSpecification<Order>
    {

        public OrderWithPaymentIntentIdSpec(string PaymentId):base(x=>x.PaymentIntentId==PaymentId)
        {
            
        }


    }
}
