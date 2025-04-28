using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(e => e.ProductName).IsRequired();
            builder.Property(e => e.UnitPrice).IsRequired().HasPrecision(10, 2);
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.Discount).IsRequired().HasPrecision(10, 2);
            builder.Property(e => e.TotalAmount).IsRequired().HasPrecision(10, 2);
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UpdatedAt).IsRequired(false);
        }
    }
}
