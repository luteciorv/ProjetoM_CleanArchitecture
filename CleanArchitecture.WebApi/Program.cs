using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Infraestructure.Communication.Extensions;
using CleanArchitecture.Infraestructure.Persistence.Extensions;
using CleanArchitecture.Infraestructure.Security.Extensions;
using CleanArchitecture.WebApi.Exceptions;
using CleanArchitecture.WebApi.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.ConfigureApplicationServices();

    // Camada de infraestrutura
    builder.Services.ConfigureCommunicationServices(builder);
    builder.Services.ConfigurePersistenceServices(builder.Configuration);
    builder.Services.ConfigureSecurityServices();

    builder.Services.ConfigureApiBehavior();
    builder.Services.ConfigureCorsPolicy();

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(ExceptionFilter));
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseHttpsRedirection();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "A execução da aplicação falhou de forma inesperada.");
}
finally
{
    Log.Information("A aplicação foi finalizada...");
    Log.CloseAndFlush();
}