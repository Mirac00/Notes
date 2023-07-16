using System.Net.Http;
using System.Windows;

namespace Notes_UI
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _client = new HttpClient();
        private string _loggedInUser;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string loggedInUser)
        {
            InitializeComponent();

            // Zapisanie nazwy zalogowanego użytkownika i aktualizacja interfejsu użytkownika
            _loggedInUser = loggedInUser;
            UpdateUI();
        }

        private void OpenNotesWindow_Click(object sender, RoutedEventArgs e)
        {
            // Przekazanie nazwy zalogowanego użytkownika do konstruktora okna NotesWindow
            NotesWindow notesWindow = new NotesWindow(_loggedInUser);
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

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Wylogowanie użytkownika za pomocą API
            var response = await _client.PostAsync("https://localhost:7202/Account/logout", null);

            if (response.IsSuccessStatusCode)
            {
                // Wyczyszczenie nazwy zalogowanego użytkownika i aktualizacja interfejsu użytkownika
                _loggedInUser = null;
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Logout failed.");
            }
        }

        private void UpdateUI()
        {
            if (!string.IsNullOrEmpty(_loggedInUser))
            {
                // Użytkownik jest zalogowany
                LoginButton.Visibility = Visibility.Collapsed;
                LogoutButton.Visibility = Visibility.Visible;
                LoggedInUserLabel.Content = $"Zalogowany jako: {_loggedInUser}";
            }
            else
            {
                // Użytkownik nie jest zalogowany
                LoginButton.Visibility = Visibility.Visible;
                LogoutButton.Visibility = Visibility.Collapsed;
                LoggedInUserLabel.Content = "";
            }
        }
    }
}




