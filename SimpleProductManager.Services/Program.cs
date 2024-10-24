using Microsoft.EntityFrameworkCore;
using Serilog;
using SimpleProductManager.Data.Entities;
using SimpleProductServices.Controllers;
using SimpleProductServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// builder.Services.Configure<ServiceConfiguration>(builder.Configuration.GetSection("ProductExport"));

// Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext());

// Helper

// DatabaseContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.EnableServiceProviderCaching(false);
    options.UseMemoryCache(null);
    options.UseSqlServer(builder.Configuration.GetConnectionString("easyqs_data") ??
                         builder.Configuration.GetConnectionString("QS_Patient"));
});

// Repositories

// Services



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SimpleProductController>();
builder.Services.AddScoped<ISimpleProductService, SimpleProductService>();
builder.Services.AddDbContext<SimpleProductDatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleProductDatabase")));

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