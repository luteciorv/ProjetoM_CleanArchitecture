using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Infraestructure.Communication.Extensions;
using CleanArchitecture.Infraestructure.Persistence.Extensions;
using CleanArchitecture.Infraestructure.Security.Extensions;
using CleanArchitecture.WebApi.Exceptions;
using CleanArchitecture.WebApi.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Camada de apresentação
builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();
builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebApi.ProjetoM",
        Version = "v1"
    });

    var xmlFile = "CleanArchitecture.WebApi.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});

// Camada de aplicação
builder.Services.ConfigureApplicationServices();

// Camada de infraestrutura
builder.Services.ConfigureCommunicationServices(builder);
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureSecurityServices();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.MapControllers();

app.Run();