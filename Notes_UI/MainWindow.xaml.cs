using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using TwoWindowApp;

namespace Notes_UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenNotesWindow_Click(object sender, RoutedEventArgs e)
        {
            // Pokaż okno notatek i ukryj bieżące okno
            NotesWindow notesWindow = new NotesWindow();
            notesWindow.Show();
            this.Hide();
        }

        private void OpenEventsWindow_Click(object sender, RoutedEventArgs e)
        {
            // Pokaż okno wydarzeń i ukryj bieżące okno
            EventsWindow eventsWindow = new EventsWindow();
            eventsWindow.Show();
            this.Hide();
        }

        private void OpenLoginWindow_Click(object sender, RoutedEventArgs e)
        {
            // Pokaż okno logowania i ukryj bieżące okno
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Hide();
        }
    }
}
