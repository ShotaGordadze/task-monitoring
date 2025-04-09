using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infrastructure.Commands.TaskCommands;
using Infrastructure.Models;

namespace TaskMonitoring
{
    public partial class MainWindow : Window
    {
        private readonly ITaskCommands _taskCommands;

        public ObservableCollection<TodoTask> Tasks { get; set; } = new();

        public MainWindow(ITaskCommands taskCommands)
        {
            _taskCommands = taskCommands;
            InitializeComponent();
            DataContext = this; // Enable data binding to Tasks collection
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTasksAsync();
        }

        private async Task LoadTasksAsync()
        {
            try
            {
                var loadedTasks = await _taskCommands.LoadTasksAsync();

                Tasks.Clear();
                foreach (var task in loadedTasks)
                    Tasks.Add(task);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load tasks:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _taskCommands.InsertTaskAsync();
                MessageBox.Show("Test insert successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                await LoadTasksAsync(); // Refresh the grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Test insert failed:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadTasksAsync();
        }

        // Click handler for the three dots button to open context menu
        private void ThreeDotButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var task = button?.DataContext as TodoTask; // Get the task for this row

            if (task != null)
            {
                var contextMenu = new ContextMenu();

                var editMenuItem = new MenuItem { Header = "Edit" };
                editMenuItem.Click += (s, args) => EditTask(task);
                contextMenu.Items.Add(editMenuItem);

                var deleteMenuItem = new MenuItem { Header = "Delete" };
                deleteMenuItem.Click += (s, args) => DeleteTask(task);
                contextMenu.Items.Add(deleteMenuItem);

                contextMenu.IsOpen = true;
            }
        }

        private void EditTask(TodoTask task)
        {
            MessageBox.Show($"Edit Task: {task.Id} - {task.Content}");
            // Implement Edit logic here (open an edit dialog, etc.)
        }

        private async void DeleteTask(TodoTask task)
        {
            var result = MessageBox.Show($"Are you sure you want to delete task '{task.Content}'?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // await _taskCommands.DeleteTaskAsync(task.Id);
                    Tasks.Remove(task); // Remove from the UI
                    MessageBox.Show("Task deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
