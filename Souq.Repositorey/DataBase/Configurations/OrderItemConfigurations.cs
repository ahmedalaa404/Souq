using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Souq.Core.Entites.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Repositorey.DataBase.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            #region Navigation One To One From To Relations 
            builder.OwnsOne(o => o.Product, Product => Product.WithOwner());
            #endregion
            #region type Of Price
            builder.Property(orderItem => orderItem.Price).HasColumnType("Decimal(18,2)"); 
            #endregion




        }
    }

}
