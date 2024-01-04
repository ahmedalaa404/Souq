using Souq.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Specification
{
    public class BaseSpecification<t> : ISpecification<t> where t : BaseEntity
    {
        public Expression<Func<t, bool>> Criteria { get; set; }
        public List<Expression<Func<t, BaseEntity>>> Includes { get; set; } = new List<Expression<Func<t, BaseEntity>>>();

        public Expression<Func<t, object>> OrderBy { get ; set ; }
        public Expression<Func<t, object>> OrderByDesc { get ; set ; }

        public BaseSpecification()
        {
            
        }


        public BaseSpecification(Expression<Func<t, bool>> criteria)
        {
          Criteria = criteria;
        }

        public void AddOrderBy(Expression<Func<t, object>> order)
        {
            OrderBy = order;
        }


        public void AddOrderByDesci(Expression<Func<t, object>> order)
        {
            OrderByDesc = order;

        }




    }
}
