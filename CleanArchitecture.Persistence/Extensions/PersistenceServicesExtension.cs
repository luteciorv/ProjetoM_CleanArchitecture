using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Infraestructure.Persistence.Context;
using CleanArchitecture.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infraestructure.Persistence.Extensions
{
    public static class PersistenceServicesExtension
    {
        public static void ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SQLServer");
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
