using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(e => e.SaleNumber).IsRequired().HasMaxLength(200);
            builder.Property(e => e.SaleDate).IsRequired();
            builder.Property(e => e.CustomerName).IsRequired();
            builder.Property(e => e.TotalAmount).IsRequired();
            builder.Property(e => e.Branch).IsRequired(false);
            builder.Property(e => e.IsCancelled).IsRequired().HasDefaultValue(false);
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UpdatedAt).IsRequired(false);

            builder.HasMany(e => e.Items)
             .WithOne()
             .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => e.SaleNumber)
                .IsUnique();
        }
    }
}
