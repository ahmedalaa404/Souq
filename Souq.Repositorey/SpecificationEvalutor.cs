using Microsoft.EntityFrameworkCore;
using Souq.Core.DataBase;
using Souq.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Repositorey
{
    public static  class SpecificationEvalutor<t> where t:BaseEntity
    {

        public static IQueryable<t> GetQuery(IQueryable<t> InputeQuery,ISpecification<t> Specification)   // take Context.Product  -----   Critera % includes
        {
            var Query=InputeQuery;
           
            if(Specification.Criteria is not null)
            {
                Query = Query.Where(Specification.Criteria); // To Make Where In Inpute 
            }


            if (Specification.IsPagination == true)
            {
                Query = Query.Skip(Specification.Skip).Take(Specification.Take);
            }








            if (Specification.OrderBy is not null)
            {
                Query = Query.OrderBy(Specification.OrderBy);
            }
            if(Specification.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(Specification.OrderByDesc);

            }
            Query = Specification.Includes.Aggregate(Query, (Q1, Include) => Q1.Include(Include));

            return Query;
        }



    }
}
