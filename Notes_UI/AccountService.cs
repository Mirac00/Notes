using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Notes_UI
{
    public class AccountService
    {
        private readonly HttpClient _client;

        public AccountService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7202/")
            };
        }

        public async Task<bool> Login(string userName, string password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserName", userName),
                new KeyValuePair<string, string>("Password", password)
            });

            var result = await _client.PostAsync("/Account/login", content);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Register(string userName, string password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserName", userName),
                new KeyValuePair<string, string>("Password", password)
            });

            var result = await _client.PostAsync("/Account/register", content);

            return result.IsSuccessStatusCode;
        }
    }
}

