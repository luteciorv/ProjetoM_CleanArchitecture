using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Infraestructure.Communication.Extensions;
using CleanArchitecture.Infraestructure.Persistence.Extensions;
using CleanArchitecture.Infraestructure.Security.Extensions;
using CleanArchitecture.WebApi.Exceptions;
using CleanArchitecture.WebApi.Extensions;

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

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.MapControllers();

app.Run();