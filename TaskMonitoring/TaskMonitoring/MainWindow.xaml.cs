using System;
using System.Windows;
using Infrastructure;
using Infrastructure.Commands.TaskCommands;

namespace TaskMonitoring
{
    public partial class MainWindow : Window
    {
        private readonly SupabaseService _service = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var taskCommands = new TaskCommands();

                taskCommands.InsertTask();
                MessageBox.Show("Supabase connection successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private async void LoadTodos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var taskCommands = new TaskCommands();
                var tasks = await taskCommands.LoadTasks();

                // Convert the list of tasks into a string to display in the MessageBox
                var taskDetails = string.Join(Environment.NewLine, tasks.Select(task => task.ToString()));

                // Show the tasks in the MessageBox
                MessageBox.Show($"Load Todos button clicked!\nTasks:\n{taskDetails}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("An error occurred while loading tasks.");
            }
        }
    }
}