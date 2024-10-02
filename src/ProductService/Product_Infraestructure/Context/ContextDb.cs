using Microsoft.EntityFrameworkCore;
using Product_Domain.Entities;
using Product_Infraestructure.Context.Configuration;

namespace Product_Infraestructure.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options) { }
        public DbSet<Product> Products { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
