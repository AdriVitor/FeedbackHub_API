using FeedbackService_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeedbackService_Infraestructure.Context.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.ToTable("FEEDBACK");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Grade)
                .HasConversion<int>();
        }
    }
}
