using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entities;
using Webshop.Domain.Enums;

namespace Webshop.Infrastructure.SQL.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.ProductCategory)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.ProductCategoryId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Price)
               .HasColumnType("decimal(18,2)"); 

        builder.Property(p => p.Status)
               .HasDefaultValue(ProductStatus.Active);
    }
}
