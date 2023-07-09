using Notes_UI;

using System.Windows;

namespace TwoWindowApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz wartości z pól loginu i hasła
            string login = loginTextBox.Text;
            string password = passwordBox.Password;

            // Wykonaj logikę weryfikacji loginu i hasła

            if (isValidLogin(login, password))
            {
                // Jeśli login i hasło są poprawne, wykonaj odpowiednie działania

                // Przykładowe działanie: Wyświetlenie komunikatu o poprawnym zalogowaniu
                MessageBox.Show("Poprawnie zalogowano!");

                // Tutaj możesz wykonać inne działania, takie jak otwarcie głównego okna aplikacji, itp.

                // Zamknij okno logowania
                Close();
            }
            else
            {
                // Jeśli login i hasło są niepoprawne, wyświetl komunikat o błędzie
                MessageBox.Show("Niepoprawny login lub hasło!");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Otwórz ponownie główne okno (MainWindow)
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Zamknij obecne okno (LoginWindow)
            Close();
        }

        // Przykładowa metoda weryfikująca login i hasło
        private bool isValidLogin(string login, string password)
        {
            // Tutaj możesz napisać swoją logikę weryfikacji loginu i hasła
            // Na potrzeby przykładu zwrócę true jeśli login i hasło są takie same
            return login == password;
        }
    }
}

