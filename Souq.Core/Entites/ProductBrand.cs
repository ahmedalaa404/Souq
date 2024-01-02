using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.DataBase
{
    public class ProductBrand:BaseEntity
    {
        public string Name { get; set; }


        // if u Don`t make That The Ef Core Is Refer That Is One 

        //public ICollection<Product> Product { get; set; } 
    }
}
