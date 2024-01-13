using Souq.Core.DataBase;
using Souq.Core.Entites.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Repositories
{
    public interface IUniteOFWork:IAsyncDisposable   // To Close The Connection of StoreContext 
    {

        #region old
        //public IGenericRepository<Product> ProductRepo { get; set; }
        //public IGenericRepository<ProductBrand> ProductBrandRepo { get; set; }
        //public IGenericRepository<ProductType> ProductTypeRepo { get; set; }

        //public IGenericRepository<DeliveryMethod> DeliveryMethodRepo { get; set; }
        //public IGenericRepository<OrderItem> OrderItemRepo { get; set; }
        //public IGenericRepository<Order> OrderRepo { get; set; } 
        #endregion


         IGenericRepository<TEntity> Repositorey<TEntity>() where TEntity : BaseEntity;


        Task<int> Complete();

    }
}
