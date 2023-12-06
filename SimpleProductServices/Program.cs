using Microsoft.EntityFrameworkCore;
using Serilog;
using SimpleProductServices.Controllers;
using SimpleProductServices.Entities;
using SimpleProductServices.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SimpleProductController>();
builder.Services.AddScoped<ISimpleProductService, SimpleProductService>();
builder.Services.AddDbContext<SimpleProductContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleProductDatabase")));

builder.Services.AddControllers();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();