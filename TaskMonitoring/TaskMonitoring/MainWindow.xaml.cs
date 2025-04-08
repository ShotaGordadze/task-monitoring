using System;
using System.Windows;
using Infrastructure;
using Infrastructure.Commands.TaskCommands;

namespace TaskMonitoring
{
    public partial class MainWindow : Window
    {
        private SupabaseService _service = new SupabaseService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _service.InitializeAsync();
                var taskCommands = new TaskCommands();

                taskCommands.InsertTask();
                MessageBox.Show("Supabase connection successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}