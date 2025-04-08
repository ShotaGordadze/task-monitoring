using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Infrastructure;

namespace TaskMonitoring
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set up the DI container
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<SupabaseService>(); // Register SupabaseService as a singleton
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Initialize SupabaseService
            var supabaseService = ServiceProvider.GetRequiredService<SupabaseService>();
            await supabaseService.InitializeAsync();

            // Show the main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}