using EnterpriseService_Domain.Entities;
using EnterpriseService_Infraestructure.ContextDb.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseService_Infraestructure.ContextDb
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Enterprise> Enterprises { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EnterpriseConfiguration());
        }
    }
}
