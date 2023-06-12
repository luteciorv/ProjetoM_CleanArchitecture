using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Infraestructure.Communication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace CleanArchitecture.Infraestructure.Communication.Extensions
{
    public static class CommunicationServicesExtension
    {
        public static void ConfigureCommunicationServices(this IServiceCollection services, WebApplicationBuilder builder)
        {          
            builder.Host.UseSerilog((ctx, lc) =>
            {
                var templateLog = new ExpressionTemplate(
                    "[{@t:HH:mm:ss} {@l:u3} " +
                    "{Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)}] {@m}\n{@x}",
                    theme: TemplateTheme.Literate
                );
                var templateFile = new ExpressionTemplate(
                   "[{@t:HH:mm:ss} {@l:u3} " +
                   "{Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)}] {@m}\n{@x}"
               );

                lc.WriteTo.Async(writeTo => writeTo.Console(templateLog, LogEventLevel.Warning));
                lc.WriteTo.Async(writeTo => writeTo.File(templateFile, "Logs/Serilog_.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day));
            });

            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
