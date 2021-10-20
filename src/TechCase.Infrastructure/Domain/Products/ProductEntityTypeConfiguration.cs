using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCase.Domain.Products;

namespace TechCase.Infrastructure.Domain.Products
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "orders");

            builder.HasKey(b => b.Id);
            builder.Property(_ => _.Name).HasColumnName("Name");
            builder.Property(_ => _.Price).HasColumnType("decimal(18,2)");
            builder.Property(_ => _.StockQuantity).HasColumnName("StockQuantity");
        }
    }
}
