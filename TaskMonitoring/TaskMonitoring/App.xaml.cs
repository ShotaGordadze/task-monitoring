using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Infrastructure;
using Infrastructure.Commands.TaskCommands;
using Microsoft.Extensions.Hosting;

namespace TaskMonitoring
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<ISupabaseService, SupabaseService>();
                    services.AddScoped<ITaskCommands, TaskCommands>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            await AppHost!.StartAsync();

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();  
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            
            await AppHost!.StopAsync();
        }
    }
}