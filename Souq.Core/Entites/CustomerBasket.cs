using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Entites
{
    public  class CustomerBasket
    {

        public string Id { get; set; }
        public BasketItem Items { get; set; }

        public CustomerBasket(string Id )
        {
           this.Id = Id;
        }

    }
}
