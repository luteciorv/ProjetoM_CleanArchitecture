using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Infraestructure.Security.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infraestructure.Security.Extensions
{
    public static class SecurityServicesExtension
    {
        public static void ConfigureSecurityServices(this IServiceCollection services)
        {         
            services.AddScoped<IPasswordService, PasswordService>();
        }
    }
}
