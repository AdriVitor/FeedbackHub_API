using EnterpriseService_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseService_Infraestructure.ContextDb.Configurations
{
    public class EnterpriseConfiguration : IEntityTypeConfiguration<Enterprise>
    {
        public void Configure(EntityTypeBuilder<Enterprise> builder)
        {
            builder.ToTable("ENTERPRISES");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnName("NAME")
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .IsRequired();

            builder.Property(e => e.CNPJ)
                .HasColumnName("CNPJ")
                .IsRequired();
        }
    }
}
