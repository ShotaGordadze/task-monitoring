using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Infrastructure;
using Infrastructure.Commands.TaskCommands;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace TaskMonitoring
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userInfo.json");
        private readonly IAuthCommands _authCommands;

        public App()
        {
            AppHost = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<UserWindow>();
                    services.AddSingleton<LoginWindow>();
                    services.AddSingleton<ISupabaseService, SupabaseService>();
                    services.AddScoped<ITaskCommands, TaskCommands>();
                    services.AddScoped<IAuthCommands, AuthCommands>();
                    services.AddScoped<IUserCommands, UserCommands>();
                })
                .Build();
            
            _authCommands = new AuthCommands();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            await AppHost!.StartAsync();
            
            var json = await File.ReadAllTextAsync(_path, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
            var currentUserJson = JsonConvert.DeserializeObject<UserInfo>(json);

            if (currentUserJson is { IsLoggedIn: "1", Role:"Moderator" })
            {
               var moderatorWindow = new ModeratorWindow();
               moderatorWindow.Show();
               return;
            }
            
            if (currentUserJson is { IsLoggedIn: "1", Role:"User" })
            {
                var userWindow = new UserWindow();
                userWindow.Show();
            }
            else
            {
                var loginWindow = AppHost.Services.GetRequiredService<LoginWindow>();
                loginWindow.Show();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            
            await AppHost!.StopAsync();
        }
    }
}