using Souq.Core.DataBase;
using Souq.Core.Entites.Order_Aggregate;
using Souq.Core.Repositories;
using Souq.Repositorey.DataBase;
using Souq.Repositorey.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Repositorey
{
    public class UniteOFWork : IUniteOFWork
    {
        private readonly StoreContext context;

        private Dictionary<string, GenericRepository<BaseEntity>> _Repositorey;
        public UniteOFWork(StoreContext Context)
        {
            context = Context;
            _Repositorey = new Dictionary<string, GenericRepository<BaseEntity>>();
        }
        #region OLD
        //public IGenericRepository<Product> ProductRepo { get ; set ; }
        //public IGenericRepository<ProductBrand> ProductBrandRepo { get ; set ; }
        //public IGenericRepository<ProductType> ProductTypeRepo { get ; set ; }
        //public IGenericRepository<DeliveryMethod> DeliveryMethodRepo { get ; set ; }
        //public IGenericRepository<OrderItem> OrderItemRepo { get ; set ; }
        //public IGenericRepository<Order> OrderRepo { get ; set ; } 
        #endregion

        public async Task<int> Complete()
        {
            await context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
       
        }

        public IGenericRepository<TEntity> Repositorey<TEntity>() where TEntity : BaseEntity
        {
            var Type=typeof(TEntity).Name;
            //_Repositorey.Add(Type, GenericRepository<Type>());


            if (_Repositorey.ContainsKey(Type))
            {
                var Repo= new GenericRepository<TEntity>(context); 
            }
        }
    }
}
