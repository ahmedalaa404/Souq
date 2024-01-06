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
    public class GetCountFromProductWithspec : BaseSpecification<Product>
    {



        public GetCountFromProductWithspec(ProductWithParam Param) : base(x =>


           (!Param.BrandId.HasValue || x.ProductBrandId == Param.BrandId)
        && (string.IsNullOrEmpty(Param.Search)|| x.Name.ToLower().Contains(Param.Search))
        && (!Param.TypeId.HasValue || x.ProductTypeId == Param.TypeId)
        )
        {


            if (!string.IsNullOrEmpty(Param.Sort))

            IsPaginationWork(Param.PageSize * (Param.PageIndex - 1), Param.PageSize);




        }


        public GetCountFromProductWithspec(int id) : base(x => x.Id == id)
        {
            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);
        }





    }
}
