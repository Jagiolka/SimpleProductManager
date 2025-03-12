using Microsoft.EntityFrameworkCore;
using Serilog;
using SimpleProductManager.Services;
using SimpleProductServices.Controllers;
using SimpleProductServices.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext());

// Helper

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SimpleProductController>();
builder.Services.AddScoped<SimpleProductCategoryController>();

builder.Services.AddScoped<ISimpleProductService, SimpleProductService>();
builder.Services.AddScoped<ISimpleProductCategoryService, SimpleProductCategoryService>();

// DatabaseContext
builder.Services.AddDbContext<SimpleProductDatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleProductDatabaseDefaultConnection")));

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