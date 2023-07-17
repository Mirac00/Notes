using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

using Notes_UI.Services;

namespace Notes_UI
{
    public partial class NotesWindow : Window
    {
        public Note SelectedNote { get; set; }
        private string _loggedInUser;

        public NotesWindow(string loggedInUser)
        {
            InitializeComponent();
            DataContext = this;

            _loggedInUser = loggedInUser;
            ApiClient.Initialize();
            RefreshNotes();
            UpdateUI();
        }

        private async void RefreshNotes()
        {
            try
            {
                List<Note> notes = await ApiClient.GetAllNotes();
                notesListBox.ItemsSource = notes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving notes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedNote = (Note)notesListBox.SelectedItem;
            if (SelectedNote != null)
            {
                titleTextBox.Text = SelectedNote.Title;
                descriptionTextBox.Text = SelectedNote.Description;

                updateButton.Visibility = Visibility.Visible;
                deleteButton.Visibility = Visibility.Visible;
            }
            else
            {
                updateButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private async void AddNote_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_loggedInUser))
            {
                Note newNote = new Note
                {
                    Title = titleTextBox.Text,
                    Description = descriptionTextBox.Text,
                    IsVisible = true
                };

                try
                {
                    Note addedNote = await ApiClient.AddNoteWithAuthentication(newNote);
                    if (addedNote != null)
                    {
                        MessageBox.Show($"Note added with ID: {addedNote.Id}", "Note Added", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshNotes();
                        ClearNoteFields();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add note. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("You need to log in to add a note.", "Login Required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void UpdateNote_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNote != null)
            {
                SelectedNote.Title = titleTextBox.Text;
                SelectedNote.Description = descriptionTextBox.Text;

                try
                {
                    Note updatedNote = await ApiClient.UpdateNoteWithAuthentication(SelectedNote.Id, SelectedNote);
                    if (updatedNote != null)
                    {
                        MessageBox.Show($"Note updated with ID: {updatedNote.Id}", "Note Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshNotes();
                        ClearNoteFields();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update note. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNote != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this note?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await ApiClient.DeleteNoteWithAuthentication(SelectedNote.Id);
                        MessageBox.Show($"Note deleted with ID: {SelectedNote.Id}", "Note Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshNotes();
                        ClearNoteFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_loggedInUser);
            mainWindow.Show();
            Close();
        }

        private void UpdateUI()
        {
            if (!string.IsNullOrEmpty(_loggedInUser))
            {
                addNoteButton.IsEnabled = true;
            }
            else
            {
                addNoteButton.IsEnabled = false;
            }
        }

        private void ClearNoteFields()
        {
            titleTextBox.Text = string.Empty;
            descriptionTextBox.Text = string.Empty;
        }
    }
}








