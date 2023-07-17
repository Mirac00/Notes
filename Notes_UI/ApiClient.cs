using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Notes_UI
{
    public static class ApiClient
    {
        private static CookieContainer cookieContainer;
        private static HttpClient client;

        public static void Initialize()
        {
            cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler { UseCookies = true, CookieContainer = cookieContainer };
            client = new HttpClient(handler);

            client.BaseAddress = new Uri("https://localhost:7202/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public static void SetCookies(Uri uri, IEnumerable<Cookie> cookies)
        {
            foreach (var cookie in cookies)
            {
                cookieContainer.Add(uri, cookie);
            }
        }

        public static string GetAntiForgeryCookie()
        {
            var cookies = cookieContainer.GetCookies(client.BaseAddress);
            var antiForgeryCookie = cookies[".AspNetCore.Antiforgery"]?.Value;
            return antiForgeryCookie;
        }

        public static async Task<List<Note>> GetAllNotes()
        {
            HttpResponseMessage response = await client.GetAsync("Notes");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Note> notes = JsonConvert.DeserializeObject<List<Note>>(data);
                return notes;
            }
            else
            {
                throw new Exception("Error retrieving notes from the API.");
            }
        }

        public static async Task<Note> GetNoteById(Guid id)
        {
            HttpResponseMessage response = await client.GetAsync($"Notes/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Note note = JsonConvert.DeserializeObject<Note>(data);
                return note;
            }
            else
            {
                throw new Exception($"Error retrieving note with ID: {id} from the API.");
            }
        }

        public static async Task<Note> AddNoteWithAuthentication(Note note)
        {
            string json = JsonConvert.SerializeObject(note);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("Notes", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Note addedNote = JsonConvert.DeserializeObject<Note>(data);
                return addedNote;
            }
            else
            {
                throw new Exception("Error adding note through the API.");
            }
        }

        public static async Task<Note> UpdateNoteWithAuthentication(Guid id, Note note)
        {
            string json = JsonConvert.SerializeObject(note);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"Notes/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Note updatedNote = JsonConvert.DeserializeObject<Note>(data);
                return updatedNote;
            }
            else
            {
                throw new Exception($"Error updating note with ID: {id} through the API.");
            }
        }

        public static async Task DeleteNoteWithAuthentication(Guid id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"Notes/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error deleting note with ID: {id} through the API.");
            }
        }
    }

    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
    }
}




