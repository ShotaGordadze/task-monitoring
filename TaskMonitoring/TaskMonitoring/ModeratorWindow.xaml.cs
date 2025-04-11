using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Infrastructure.Commands.TaskCommands;
using Infrastructure.Models;
using Newtonsoft.Json;

namespace TaskMonitoring;

public partial class ModeratorWindow : Window
{
    private readonly ITaskCommands _taskCommands;
    private readonly IAuthCommands _authCommands;
    private readonly IUserCommands _userCommands;

    private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userInfo.json");

    public ObservableCollection<TodoTask> Tasks { get; set; } = new();
    private List<AppUser> _allUsers;


    public string DisplayName { get; set; } = "👤";


    public ModeratorWindow()
    {
        _taskCommands = new TaskCommands();
        _authCommands = new AuthCommands();
        _userCommands = new UserCommands();
        InitializeComponent();
        DataContext = this;
        Loaded += MainWindow_Loaded;
        LoadUserInfo();
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadTasksAsync();
        await LoadUsersForFilter();
    }

    private async Task LoadTasksAsync()
    {
        try
        {
            var json = await File.ReadAllTextAsync(_path, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
            var currentUser = JsonConvert.DeserializeObject<UserInfo>(json);

            var loadedTasks = await _taskCommands.LoadTasksAsync( /*ToDo*/);

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

    private async Task LoadTasksAsync(string username)
    {
        try
        {
            List<TodoTask> loadedTasks;

            username ??= "all";

            if (username == "all")
                loadedTasks = await _taskCommands.LoadTasksAsync();
            else
                loadedTasks = await _taskCommands.LoadTasksAsync(username);


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

    private async void ReloadButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedUsername = UserFilterComboBox.SelectedValue?.ToString();
        selectedUsername ??= "all";
        await LoadTasksAsync(selectedUsername);
    }

    private async void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        await _authCommands.LogOut();

        var loginWindow = new LoginWindow();
        loginWindow.Show();
        Close();
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

    private async Task LoadUsersForFilter()
    {
        var users = await _userCommands.GetAllUsersAsync();

        _allUsers = new List<AppUser>
        {
            new AppUser { Username = "all", DisplayName = "ყველა" }
        };

        _allUsers.AddRange(users.Select(u => new AppUser
        {
            Username = u.UserName,
            DisplayName = $"{u.Name} {u.LastName}"
        }));

        UserFilterComboBox.ItemsSource = _allUsers;
        UserFilterComboBox.DisplayMemberPath = "DisplayName";
        UserFilterComboBox.SelectedValuePath = "Username";
        UserFilterComboBox.SelectedValue = "all";
    }

    private async void UserFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedUsername = UserFilterComboBox.SelectedValue?.ToString();
        await LoadTasksAsync(selectedUsername);
    }
}

public class AppUser
{
    public string Username { get; set; }
    public string DisplayName { get; set; }
}