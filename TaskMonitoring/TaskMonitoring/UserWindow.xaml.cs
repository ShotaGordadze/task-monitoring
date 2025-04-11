using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Infrastructure.Commands.TaskCommands;
using Infrastructure.Models;
using Newtonsoft.Json;

namespace TaskMonitoring
{
    public partial class UserWindow
    {
        private readonly ITaskCommands _taskCommands;
        private readonly IAuthCommands _authCommands;

        private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userInfo.json");

        public ObservableCollection<TodoTask> Tasks { get; set; } = new();

        public string DisplayName { get; set; } = "👤";


        public UserWindow()
        {
            _taskCommands = new TaskCommands();
            _authCommands = new AuthCommands();
            InitializeComponent();
            DataContext = this;
            Loaded += MainWindow_Loaded;
            LoadUserInfo();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadUserTasksAsync();
        }

        private async Task LoadUserTasksAsync()
        {
            try
            {
                var json = File.ReadAllText(_path, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
                var currentUser = JsonConvert.DeserializeObject<UserInfo>(json) ??
                                  throw new Exception("მომხმარებელი არ არსებობს");

                var loadedTasks = await _taskCommands.LoadTasksAsync(currentUser.Id);

                Tasks.Clear();
                foreach (var task in loadedTasks)
                    Tasks.Add(task);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load tasks:\n" + ex.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void InsertTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var insertTaskWindow = new InsertTaskWindow(_taskCommands, _authCommands);
                insertTaskWindow.Show();
                await LoadUserTasksAsync(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Test insert failed:\n" + ex.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadUserTasksAsync();
        }

        // Click handler for the three dots button to open context menu
        private void ThreeDotButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button?.DataContext is not TodoTask task) return;
            var contextMenu = new ContextMenu();

            var editMenuItem = new MenuItem { Header = "რედაქტირება" };
            editMenuItem.Click += (s, args) => EditTask(task);
            contextMenu.Items.Add(editMenuItem);

            var deleteMenuItem = new MenuItem { Header = "წაშლა" };
            deleteMenuItem.Click += (s, args) => DeleteTask(task);
            contextMenu.Items.Add(deleteMenuItem);

            contextMenu.IsOpen = true;
        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            await _authCommands.LogOut();

            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void EditTask(TodoTask task)
        {
            var editWindow = new EditTaskWindow(task);
            editWindow.Show();
        }

        private async void DeleteTask(TodoTask task)
        {
            var result = MessageBox.Show($"Are you sure you want to delete task '{task.Content}'?",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _taskCommands.DeleteTaskAsync(task.Id);
                    Tasks.Remove(task);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting task: {ex.Message}", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void LoadUserInfo()
        {
            var json = File.ReadAllText(_path, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
            var currentUser = JsonConvert.DeserializeObject<UserInfo>(json);

            FullNameText.Text = $"სახელი: {currentUser.FirtsName} {currentUser.LastName}";
            RoleText.Text = $"როლი: {currentUser.Role}";
            UsernameText.Text = $"მომხმარებელი: {currentUser.Username}";
            DisplayName += $" {currentUser.FirtsName} {currentUser.LastName}";
        }
    }

    internal class UserInfo
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")] public string FirtsName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        
        [JsonProperty(PropertyName = "is_logged_in")] 
        public string IsLoggedIn { get; set; }
    }
}