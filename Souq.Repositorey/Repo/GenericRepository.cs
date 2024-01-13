using Microsoft.EntityFrameworkCore;
using Souq.Core.DataBase;
using Souq.Core.Repositories;
using Souq.Core.Specification;
using Souq.Repositorey.DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Repositorey.Repo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity

    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext Context) //Ask ClR To Make Instance Implicetly
        {
            context = Context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsyc()
        {

            if(typeof(T)==typeof(Product))
            {
                var AllProduct = await context.Set<Product>().Include(x=>x.ProductBrand).Include(x=>x.ProductType).ToListAsync() as IReadOnlyList<T>;
                return AllProduct ;

            }
            var AllData=await context.Set<T>().ToListAsync();
            return AllData;
        }


        public async Task<T> GetByIdAsync(int Id)
        {

            //return await context.Set<T>().Where(x=>x.Id == Id).FirstOrDefaultAsync();// Search Remotly in All Steps

            return await context.Set<T>().FindAsync(Id); // have Warning IF Id Is Null Here

        }



        #region Function Dynamic With Specification
        public async Task<IReadOnlyList<T>> GetAllAsycWithSpec(ISpecification<T> Spec)
        {
          return await ApplySpecification(Spec).ToListAsync();
        }

        public async Task<T> GetByIdAsyncWithSpec(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }
        #endregion





        #region ApplySpecification


        IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvalutor<T>.GetQuery(context.Set<T>(), Spec);
        }

        public async Task Add(T Entity)
        {
           await context.Set<T>().AddAsync(Entity);
        }

        public void Update(T Entity)
        {
             context.Set<T>().Update(Entity);

        }

        public void Delete(T Entity)
        {
            context.Set<T>().Remove(Entity);
        }

        #endregion





    }
}
