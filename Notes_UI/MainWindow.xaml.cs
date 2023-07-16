using System.Windows;

using TwoWindowApp;

namespace Notes_UI
{
    public partial class MainWindow : Window
    {
        private string loggedInUser;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
        }

        private void OpenNotesWindow_Click(object sender, RoutedEventArgs e)
        {
            NotesWindow notesWindow = new NotesWindow();
            notesWindow.Show();
            this.Hide();
        }

        private void OpenEventsWindow_Click(object sender, RoutedEventArgs e)
        {
            EventsWindow eventsWindow = new EventsWindow();
            eventsWindow.Show();
            this.Hide();
        }

        private void OpenLoginWindow_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Hide();
        }
    }
}

