using Souq.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Souq.Api.Helper;

namespace Souq.Core.Specification
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {



        public ProductWithBrandAndTypeSpecification(ProductWithParam Param) : base(x =>


                (!Param.BrandId.HasValue || x.ProductBrandId == Param.BrandId)
        && (string.IsNullOrEmpty(Param.Search)|| x.Name.ToLower().Contains(Param.Search))
        && (!Param.TypeId.HasValue || x.ProductTypeId == Param.TypeId)
        )
        {


            if (!string.IsNullOrEmpty(Param.Sort))
            {
                switch (Param.Sort.ToLower())
                {
                    case "price":
                        AddOrderBy(x => x.Price);
                        break;

                    case "pricedesc":
                        AddOrderByDesci(x => x.Price);
                        break;


                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }

            }

            IsPaginationWork(Param.PageSize * (Param.PageIndex - 1), Param.PageSize);



            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);
        }


        public ProductWithBrandAndTypeSpecification(int id) : base(x => x.Id == id)
        {
            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);
        }





    }
}
