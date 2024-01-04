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



        public ProductWithBrandAndTypeSpecification(string? Sort, int? BrandId, int? typeId) : base(x =>


                (!BrandId.HasValue || x.ProductBrandId == BrandId) && (!typeId.HasValue || x.ProductTypeId == typeId)

        )
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
