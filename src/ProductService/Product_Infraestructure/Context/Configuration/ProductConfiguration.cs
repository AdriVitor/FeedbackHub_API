using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product_Domain.Entities;

namespace Product_Infraestructure.Context.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUCTS");

            builder.HasKey(p => new { p.Id, p.IdEnterprise});

            builder.Property(p => p.Name)
                .HasColumnName("NAME")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName("DESCRIPTION")
                .HasColumnType("VARCHAR")
                .HasMaxLength(300);

            builder.Property(p => p.IdEnterprise)
                .HasColumnName("IDENTERPRISE")
                .HasColumnType("INT");

            builder.Property(p => p.IsActive)
                .HasColumnName("ISACTIVE")
                .HasColumnType("BIT")
                .IsRequired();
        }
    }
}
