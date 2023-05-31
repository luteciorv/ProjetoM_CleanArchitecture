using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Extensions;
using CleanArchitecture.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInfraServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();

builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
dataContext?.Database.EnsureCreated();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();
app.UseErrorHandler();
app.UseCors();

app.MapControllers();

app.Run();