using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Notes_UI
{
    public partial class NotesWindow : Window
    {
        public Note SelectedNote { get; set; }

        public NotesWindow()
        {
            InitializeComponent();
            DataContext = this;

            RefreshNotes();
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

                // Pokaż przyciski Aktualizuj i Usuń
                updateButton.Visibility = Visibility.Visible;
                deleteButton.Visibility = Visibility.Visible;
            }
            else
            {
                // Ukryj przyciski Aktualizuj i Usuń
                updateButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private async void AddNote_Click(object sender, RoutedEventArgs e)
        {
            Note newNote = new Note
            {
                Title = titleTextBox.Text,
                Description = descriptionTextBox.Text,
                IsVisible = true
            };

            try
            {
                Note addedNote = await ApiClient.AddNote(newNote);
                MessageBox.Show($"Note added with ID: {addedNote.Id}", "Note Added", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshNotes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    Note updatedNote = await ApiClient.UpdateNote(SelectedNote.Id, SelectedNote);
                    MessageBox.Show($"Note updated with ID: {updatedNote.Id}", "Note Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshNotes();
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
                        await ApiClient.DeleteNote(SelectedNote.Id);
                        MessageBox.Show($"Note deleted with ID: {SelectedNote.Id}", "Note Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshNotes();
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

    }
}

