using System.Reflection;

using keyforger.infrastructure;

using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// look for all handlers etc in this assembly
builder.Services.AddMediatR(
  typeof(keyforger.application.Extensions).GetTypeInfo()
    .Assembly, 
  typeof(Extensions).GetTypeInfo()
    .Assembly);

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();