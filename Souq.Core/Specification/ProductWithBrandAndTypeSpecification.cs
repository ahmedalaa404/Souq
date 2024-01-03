using Souq.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Specification
{
    public class ProductWithBrandAndTypeSpecification:BaseSpecification<Product>
    {



        public ProductWithBrandAndTypeSpecification()
        {

            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);
        }


        public ProductWithBrandAndTypeSpecification(int id) : base(x=>x.Id==id)
        {
            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);
        }





    }
}
