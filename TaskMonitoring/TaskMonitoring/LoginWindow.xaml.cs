using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Infrastructure;
using Infrastructure.Commands.TaskCommands;
using Infrastructure.Models;

namespace TaskMonitoring;

public partial class LoginWindow : Window
{
    private readonly IAuthCommands _authCommands;

    public LoginWindow()
    {
        _authCommands = new AuthCommands();
        InitializeComponent();
    }

    private void RegisterText_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var sb = (Storyboard)Resources["FlipToRegister"];
        sb.Begin();
    }

    private void BackToLoginText_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var sb = (Storyboard)Resources["FlipToLogin"];
        sb.Begin();
    }

    private void FlipToRegister_Completed(object sender, EventArgs e)
    {
        LoginPanel.Visibility = Visibility.Collapsed;
        RegisterPanel.Visibility = Visibility.Visible;
    }

    private void FlipToLogin_Completed(object sender, EventArgs e)
    {
        RegisterPanel.Visibility = Visibility.Collapsed;
        LoginPanel.Visibility = Visibility.Visible;
    }

    public async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var username = UsernameTextBox.Text.Trim();
        var password = PasswordBox.Password.Trim();

        try
        {
           var user = await _authCommands.LogIn(username, password);

           switch (user.RoleId)
           {
               case 1: //Moderator
               {
                   var moderatorWindow = new ModeratorWindow();
                   Application.Current.MainWindow = moderatorWindow;
                   moderatorWindow.Show();
                   break;
               }
               case 2: //User
               {
                   var userWindow = new UserWindow();
                   Application.Current.MainWindow = userWindow;
                   userWindow.Show();
                   break;
               }
           }

           Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "შეცდომა", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private async void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        var firstName = FirstNameTextBox.Text.Trim();
        var lastName = LastNameTextBox.Text.Trim();
        var username = RegisterUsernameTextBox.Text.Trim();
        var password = RegisterPasswordBox.Password.Trim();
        var tempRole = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content!;

        var role = tempRole switch
        {
            "მოდერატორი" => new Role
            {
                Id = 1,
                RoleName = Roles.Moderator
            },

            "თანამშრომელი" => new Role
            {
                Id = 2,
                RoleName = Roles.User
            },

            _ => throw new Exception("არასწორი როლია არჩეული.")
        };

        if (string.IsNullOrEmpty(firstName) ||
            string.IsNullOrEmpty(lastName) ||
            string.IsNullOrEmpty(username) ||
            string.IsNullOrEmpty(password))
        {
            MessageBox.Show("გთხოვთ შეავსოთ ყველა ველი.", "შეცდომა", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        password = _authCommands.EncryptPassword(password);

        var user = new User()
        {
            Name = firstName,
            LastName = lastName,
            UserName = username,
            Password = password,
            RoleId = role.Id
        };
        try
        {
            var insertedUser = await _authCommands.SignIn(user);

            switch (insertedUser.RoleId)
            {
                case 1: //Moderator
                {
                    var moderatorWindow = new ModeratorWindow();
                    Application.Current.MainWindow = moderatorWindow;
                    moderatorWindow.Show();
                    break;
                }
                case 2: //User
                {
                    var userWindow = new UserWindow();
                    Application.Current.MainWindow = userWindow;
                    userWindow.Show();
                    break;
                }
            }

            Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "შეცდომა", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}