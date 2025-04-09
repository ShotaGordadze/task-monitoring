using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace TaskMonitoring;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }
    
    private void RegisterText_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Storyboard sb = (Storyboard)this.Resources["FlipToRegister"];
        sb.Begin();
    }

    private void BackToLoginText_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Storyboard sb = (Storyboard)this.Resources["FlipToLogin"];
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

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Login clicked!");
    }
}