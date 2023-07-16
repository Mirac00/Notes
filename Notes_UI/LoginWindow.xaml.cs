using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace Notes_UI
{
    public partial class LoginWindow : Window
    {
        private readonly HttpClient _client = new HttpClient();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobranie pliku cookie antyprzekierowania
            await _client.GetAsync("https://localhost:7202/Account/Login");
            var antiForgeryToken = ApiClient.GetAntiForgeryCookie();

            var loginModel = new
            {
                UserName = UsernameTextBox.Text,
                Password = PasswordBox.Password
            };

            var json = JsonSerializer.Serialize(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Ustawienie nagłówka RequestVerificationToken z wartością pliku cookie antyprzekierowania
            content.Headers.Add("RequestVerificationToken", antiForgeryToken);

            var response = await _client.PostAsync("https://localhost:7202/Account/Login", content);

            if (response.IsSuccessStatusCode)
            {
                // Pobranie plików cookie z odpowiedzi API
                var cookies = response.Headers.GetValues("Set-Cookie");

                // Przekazanie plików cookie do klasy ApiClient
                ApiClient.SetCookies(response.RequestMessage.RequestUri, cookies.Select(c => new Cookie(c.Split('=')[0], c.Split('=')[1].Split(';')[0])));

                var mainWindow = new MainWindow(loginModel.UserName);
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid login attempt.");
            }
        }
    }
}













