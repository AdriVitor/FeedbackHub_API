using FeedbackService_Domain.Entities;
using FeedbackService_Infraestructure.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService_Infraestructure.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options) { }

        public DbSet<Feedback> Feedbacks { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
        }
    }
}
