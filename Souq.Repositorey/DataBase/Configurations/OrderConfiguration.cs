using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Souq.Core.Entites.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Repositorey.DataBase.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            #region One To One Total of to relations
            builder.OwnsOne(o => o.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

            #endregion

            #region Data Store 

            builder.Property(o => o.Status).HasConversion(

                OStatus => OStatus.ToString(),

                DataReturns => (OrderStatus)Enum.Parse(typeof(OrderStatus), DataReturns)

                );
            #endregion


            #region Total Price
            builder.Property(o => o.SubTotal).HasColumnType("Decimal(18,2)"); 
            #endregion








        }
    }
}
