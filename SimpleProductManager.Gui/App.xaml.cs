namespace SimpleProductManager.Gui;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

public partial class App : Application
{
  private readonly IHost _host;

  public App()
  {
    _host = Host.CreateDefaultBuilder()
      .ConfigureAppConfiguration((context, builder) =>
      {
        var env = context.HostingEnvironment.EnvironmentName;
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
      })
      .ConfigureServices((context, services) =>
      {
        services.AddLogging();
        services.AddSingleton<MainWindow>();
      })

      .Build();
  }

  protected override async void OnStartup(StartupEventArgs e)
  {
    await _host.StartAsync();
    var mainWindow = _host.Services.GetRequiredService<MainWindow>();
    mainWindow.Show();
    base.OnStartup(e);
  }

  protected override async void OnExit(ExitEventArgs e)
  {
    using (_host)
    {
      await _host.StopAsync(TimeSpan.FromSeconds(5));
    }
    base.OnExit(e);
  }
}
