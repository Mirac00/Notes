using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Notes_UI.Services
{
    public class AccountService
    {
        private readonly HttpClient _client;

        public AccountService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("https://localhost:7202/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> RegisterAsync(string userName, string password)
        {
            var model = new { UserName = userName, Password = password };
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("account/register", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            var model = new { UserName = userName, Password = password };
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("account/login", content);

            return response.IsSuccessStatusCode;
        }

        public async Task LogoutAsync()
        {
            await _client.PostAsync("account/logout", null);
        }
    }
}



