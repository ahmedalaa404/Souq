using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Souq.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Repositorey.DataBase.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand).WithMany()
                .HasForeignKey(x=>x.ProductBrandId);
            builder.HasOne(p => p.ProductType).WithMany()
                .HasForeignKey(x => x.ProductTypeId);

            builder.Property(X=>X.Name).IsRequired();
            builder.Property(X=>X.Description).IsRequired();
            builder.Property(X=>X.PictureUrl).IsRequired();

            // to make  not Warning

            builder.Property(X => X.Price).HasColumnType("Decimal(18,2)");

        }
    }
}
