using Notes_UI;

using System.Windows;

namespace TwoWindowApp
{
    public partial class RegisterWindow : Window
    {
        private readonly AccountService _accountService;

        public RegisterWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameTextBox.Text;
            string password = PasswordBox.Password;

            bool result = await _accountService.Register(userName, password);

            if (result)
            {
                MessageBox.Show("Rejestracja przebiegła pomyślnie!");
                Close();
            }
            else
            {
                MessageBox.Show("Wystąpił błąd podczas rejestracji.");
            }
        }
    }
}

