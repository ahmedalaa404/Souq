using Souq.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Specification
{
    public interface ISpecification<t> where t:BaseEntity // For  Query that Query is Must DbSets
    {

         Expression<Func<t, bool>>  Criteria{ get; set; } // That Is Where <Filteration>


        // U can Hae More Includes That Must IT List
        List<Expression<Func<t,BaseEntity>>> Includes { get; set; }


        Expression<Func<t,object>> OrderBy { get; set; }
        Expression<Func<t,object>> OrderByDesc { get; set; }

    }
}
