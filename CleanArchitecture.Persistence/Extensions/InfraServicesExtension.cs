using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Repositories;
using CleanArchitecture.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistence.Extensions
{
    public static class InfraServicesExtension
    {
        public static void ConfigureInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SQLServer");
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordService, PasswordService>();
        }
    }
}
