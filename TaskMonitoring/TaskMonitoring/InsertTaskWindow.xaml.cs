using System.Windows;
using System.Windows.Controls;
using Infrastructure.Models;
using Infrastructure.Commands.TaskCommands;

namespace TaskMonitoring
{
    public partial class InsertTaskWindow : Window
    {
        private readonly ITaskCommands _taskCommands;
       
        public InsertTaskWindow(ITaskCommands taskCommands)
        {
            _taskCommands = taskCommands;
            InitializeComponent();
        }

        private async void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            // Create a new task based on user input
            var newTask = new TodoTask
            {
                Project = (ProjectComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()!,
                Content = ContentTextBox.Text,
                StartDate = StartDatePicker.SelectedDate ?? DateTime.Now,
                EndDate = EndDatePicker.SelectedDate ?? DateTime.Now,
                Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()!,
                Comment = CommentTextBox.Text
            };

            try
            {
                await _taskCommands.InsertTaskAsync(newTask);

                MessageBox.Show("Task inserted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Close the insert form
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}