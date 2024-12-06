using FeedbackService_Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeedbackService_Infraestructure.Extensions
{
    public static class IServiceCollectionExtensions
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
