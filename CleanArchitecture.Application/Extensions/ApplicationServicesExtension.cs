using System.Reflection;
using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Application.Resources.UserResources.ConfirmUserEmail;
using CleanArchitecture.Application.Resources.UserResources.CreateUser;
using CleanArchitecture.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IHandler<CreateUserRequest, CreateUserResponse>, CreateUserHandler>();
            services.AddTransient<IHandler<ConfirmUserEmailRequest, ConfirmUserEmailResponse>, ConfirmUserEmailHandler>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}
