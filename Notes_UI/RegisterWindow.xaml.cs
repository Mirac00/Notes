using System;
using System.Windows;

using Notes_UI.Services;

namespace Notes_UI
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
            var userName = UserNameTextBox.Text;
            var password = PasswordBox.Password;

            try
            {
                var (result, errorMessage) = await _accountService.RegisterAsync(userName, password);

                if (result)
                    StatusLabel.Content = "Dodano nowego użytkownika";
                else
                    StatusLabel.Content = errorMessage;
            }
            catch (Exception ex)
            {
                StatusLabel.Content = $"Wystąpił błąd: {ex.Message}";
            }
        }



        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

