using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product_Infraestructure.Context;
using Product_Infraestructure.Repositories.Interfaces;
using Product_Infraestructure.Repositories;

namespace Product_Infraestructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraestructureDBSqlServer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ContextDb>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return serviceCollection;
        }
    }
}
