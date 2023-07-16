using Notes_UI;

using System;
using System.Windows;

namespace TwoWindowApp
{
    public partial class LoginWindow : Window
    {
        private string loggedInUser;
        private DateTime loginTime;
        private TimeSpan sessionDuration = TimeSpan.FromHours(1);

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordBox.Password;

            if (isValidLogin(login, password))
            {
                loggedInUser = login;
                loginTime = DateTime.Now;

                if (CheckSessionExpiration())
                {
                    MessageBox.Show("Sesja wygasła. Zaloguj się ponownie.");
                    return;
                }

                MessageBox.Show("Poprawnie zalogowano!");

                MainWindow mainWindow = new MainWindow(loggedInUser);
                mainWindow.Show();

                Close();
            }
            else
            {
                MessageBox.Show("Niepoprawny login lub hasło!");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
        }

        private bool isValidLogin(string login, string password)
        {
            // Wykonaj logikę weryfikacji loginu i hasła
            return login == password;
        }

        private bool CheckSessionExpiration()
        {
            return DateTime.Now - loginTime > sessionDuration;
        }
    }
}



