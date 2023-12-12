namespace SimpleProductManager.Gui;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleProductManager.DataLayer;
using SimpleProductManager.Gui.View;
using SimpleProductManager.Gui.ViewModel;
using System.Windows;

public partial class App : Application
{
    private readonly IHost host;

    public App()
    {
        host = Host.CreateDefaultBuilder()
          .ConfigureAppConfiguration((context, builder) =>
          {
              var env = context.HostingEnvironment.EnvironmentName;
              builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
          })
          .ConfigureServices((context, services) =>
          {
              services.AddLogging();
              services.AddScoped<IHttpClientManager, HttpClientManager>();

              services.AddScoped<MainWindowViewModel>();
              services.AddScoped<ProductEditorViewModel>();

              services.AddSingleton<MainWindow>(provider => new MainWindow
              {
                  DataContext = provider.GetRequiredService<MainWindowViewModel>()
              });

              services.AddTransient<ProductEditorWindow>(provider => new ProductEditorWindow 
              {
                  DataContext = provider.GetRequiredService<ProductEditorViewModel>()
              });
          })
          .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await host.StartAsync();
        var mainWindow = host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (host)
        {
            await host.StopAsync();
        }
        base.OnExit(e);
    }
}
