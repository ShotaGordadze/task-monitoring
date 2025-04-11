using System;
using System.Windows;
using System.Windows.Controls;
using Infrastructure.Commands.TaskCommands;
using Infrastructure.Models;

namespace TaskMonitoring
{
    public partial class EditTaskWindow : Window
    {
        private readonly TodoTask _task;
        private readonly  ITaskCommands _taskCommands;

        public EditTaskWindow(TodoTask taskToEdit)
        {
            InitializeComponent();
            _task = taskToEdit;
            LoadTaskData();
            _taskCommands = new TaskCommands();
        }

        private void LoadTaskData()
        {
            // Select project
            foreach (ComboBoxItem item in ProjectComboBox.Items)
            {
                if ((string)item.Content == _task.Project)
                {
                    ProjectComboBox.SelectedItem = item;
                    break;
                }
            }

            ContentTextBox.Text = _task.Content;
            StartDatePicker.SelectedDate = _task.StartDate;
            EndDatePicker.SelectedDate = _task.EndDate;

            foreach (ComboBoxItem item in StatusComboBox.Items)
            {
                if ((string)item.Content == _task.Status)
                {
                    StatusComboBox.SelectedItem = item;
                    break;
                }
            }

            CommentTextBox.Text = _task.Comment;
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            _task.Project = ((ComboBoxItem)ProjectComboBox.SelectedItem)?.Content.ToString();
            _task.Content = ContentTextBox.Text;
            _task.StartDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
            _task.EndDate = EndDatePicker.SelectedDate ?? DateTime.MinValue;
            _task.Status = ((ComboBoxItem)StatusComboBox.SelectedItem)?.Content.ToString();
            _task.Comment = CommentTextBox.Text;

            await _taskCommands.EditTaskAsync(_task);

            Close();
        }
    }
}