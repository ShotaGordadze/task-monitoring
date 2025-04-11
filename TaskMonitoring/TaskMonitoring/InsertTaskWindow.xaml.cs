using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Infrastructure.Models;
using Infrastructure.Commands.TaskCommands;
using Newtonsoft.Json;

namespace TaskMonitoring
{
    public partial class InsertTaskWindow : Window
    {
        private readonly ITaskCommands _taskCommands;
        private readonly IAuthCommands _authCommands;
        private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userInfo.json");

        public InsertTaskWindow(ITaskCommands taskCommands, IAuthCommands authCommands)
        {
            _taskCommands = taskCommands;
            _authCommands = authCommands;
            InitializeComponent();
        }

        private async void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            var json = await File.ReadAllTextAsync(_path, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
            var currentUserJson = JsonConvert.DeserializeObject<UserInfo>(json)
                                  ?? throw new NullReferenceException();

            var currentUser = await _authCommands.GetUserAsync(currentUserJson.Id);

            var newTask = new TodoTask
            {
                UserId = currentUser.Id,
                Initiator = currentUser.Name + " " + currentUser.LastName,
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
                
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting task: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}