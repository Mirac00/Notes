using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Notes_UI
{
    public static class ApiClientEvent
    {
        private static HttpClient client = new HttpClient();

        static ApiClientEvent()
        {
            client.BaseAddress = new Uri("https://localhost:7202/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<List<Event111>> GetAllEvents()
        {
            HttpResponseMessage response = await client.GetAsync("Event111s");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Event111> events = JsonConvert.DeserializeObject<List<Event111>>(data);
                return events;
            }
            else
            {
                throw new Exception("Error retrieving events from the API.");
            }
        }

        public static async Task<Event111> GetEventById(Guid id)
        {
            HttpResponseMessage response = await client.GetAsync($"Event111s/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Event111 event111 = JsonConvert.DeserializeObject<Event111>(data);
                return event111;
            }
            else
            {
                throw new Exception($"Error retrieving event with ID: {id} from the API.");
            }
        }

        public static async Task<Event111> AddEvent(Event111 event111)
        {
            string json = JsonConvert.SerializeObject(event111);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("Event111s", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Event111 addedEvent = JsonConvert.DeserializeObject<Event111>(data);
                return addedEvent;
            }
            else
            {
                throw new Exception("Error adding event through the API.");
            }
        }

        public static async Task<Event111> UpdateEvent(Guid id, Event111 event111)
        {
            string json = JsonConvert.SerializeObject(event111);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"Event111s/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Event111 updatedEvent = JsonConvert.DeserializeObject<Event111>(data);
                return updatedEvent;
            }
            else
            {
                throw new Exception($"Error updating event with ID: {id} through the API.");
            }
        }

        public static async Task DeleteEvent(Guid id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"Event111s/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error deleting event with ID: {id} through the API.");
            }
        }
    }

    public class Event111
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsVisible { get; set; }
    }
}
