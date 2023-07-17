using System;
using System.Collections.Generic;
using System.Linq;
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
            _client.BaseAddress = new System.Uri("https://localhost:7202/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<(bool, string)> RegisterAsync(string userName, string password)
        {
            var model = new { UserName = userName, Password = password };
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("Account/register", content);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var errorJson = await response.Content.ReadAsStringAsync();
            var errorData = JsonConvert.DeserializeObject<List<ErrorData>>(errorJson);

            var errorMessage = string.Join(Environment.NewLine, errorData.Select(x => x.Description));

            return (false, errorMessage);
        }

        public class ErrorData
        {
            public string Code { get; set; }
            public string Description { get; set; }
        }



        public async Task<bool> LoginAsync(string userName, string password)
        {
            var model = new { UserName = userName, Password = password };
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("Account/login", content);

            return response.IsSuccessStatusCode;
        }

        public async Task LogoutAsync()
        {
            await _client.PostAsync("Account/logout", null);
        }
    }
}



