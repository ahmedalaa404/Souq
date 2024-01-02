using Microsoft.EntityFrameworkCore;
using Souq.Core.DataBase;
using Souq.Core.Repositories;
using Souq.Repositorey.DataBase;
using System;
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
        public async Task<IEnumerable<T>> GetAllAsyc()
        {
            var AllData=await context.Set<T>().ToListAsync();
            return AllData;
        }

        public async Task<T> GetByIdAsync(int Id)
        {

            //return await context.Set<T>().Where(x=>x.Id == Id).FirstOrDefaultAsync();// Search Remotly in All Steps

            return await context.Set<T>().FindAsync(Id); // have Warning IF Id Is Null Here

        }
    }
}
