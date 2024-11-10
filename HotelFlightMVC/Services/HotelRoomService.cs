using HotelFlightMVC.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HotelFlightMVC.Services
{
    public class HotelRoomService
    {
        private readonly HttpClient _httpClient;

        public HotelRoomService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all available hotel rooms
        public async Task<IEnumerable<HotelRoom>> GetAllHotelRooms()
        {
            var response = await _httpClient.GetAsync("api/HotelRoom");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<HotelRoom>>(jsonData);
            }
            return null;
        }

        // Get a single hotel room for booking (Buy equivalent)
        public async Task<HotelRoom> GetHotelRoom(int id)
        {
            var response = await _httpClient.GetAsync($"api/HotelRoom/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<HotelRoom>(jsonData);
            }
            return null;
        }

        // Create a new hotel room
        public async Task<bool> CreateHotelRoom(HotelRoom hotelRoom)
        {
            var jsonData = JsonConvert.SerializeObject(hotelRoom);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/HotelRoom", content);
            return response.IsSuccessStatusCode;
        }

        // Update a hotel room
        public async Task<bool> UpdateHotelRoom(HotelRoom hotelRoom)
        {
            var jsonData = JsonConvert.SerializeObject(hotelRoom);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/HotelRoom/{hotelRoom.Id}", content);
            return response.IsSuccessStatusCode;
        }

        // Delete a hotel room
        public async Task<bool> DeleteHotelRoom(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/HotelRoom/{id}");
            return response.IsSuccessStatusCode;
        }

        // Handle the hotel room booking process (Buy equivalent)
        public async Task<bool> BookHotelRoom(int id)
        {
            var response = await _httpClient.GetAsync($"api/HotelRoom/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
