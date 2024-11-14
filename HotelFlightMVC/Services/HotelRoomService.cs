using HotelFlightMVC.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelFlightMVC.Services
{
    public class HotelRoomService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HotelRoomService> _logger;

        public HotelRoomService(HttpClient httpClient, ILogger<HotelRoomService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Get all available hotel rooms
        public async Task<IEnumerable<HotelRoom>> GetAllHotelRoomsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://hotelflightapi.onrender.com/api/HotelRoom");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<HotelRoom>>(jsonData);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching hotel rooms.");
                throw;
            }
        }

        // Get a single hotel room for booking (Buy equivalent)
        public async Task<HotelRoom> GetHotelRoomAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://hotelflightapi.onrender.com/api/HotelRoom/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<HotelRoom>(jsonData);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the hotel room with ID {id}.");
                throw;
            }
        }

        // Create a new hotel room
        public async Task<bool> CreateHotelRoomAsync(HotelRoom hotelRoom)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(hotelRoom);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://hotelflightapi.onrender.com/api/HotelRoom", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new hotel room.");
                throw;
            }
        }

        // Update a hotel room
        public async Task<bool> UpdateHotelRoomAsync(HotelRoom hotelRoom)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(hotelRoom);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://hotelflightapi.onrender.com/api/HotelRoom/{hotelRoom.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the hotel room with ID {hotelRoom.Id}.");
                throw;
            }
        }

        // Delete a hotel room
        public async Task<bool> DeleteHotelRoomAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://hotelflightapi.onrender.com/api/HotelRoom/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the hotel room with ID {id}.");
                throw;
            }
        }

        // Handle the hotel room booking process (Buy equivalent)
        public async Task<bool> BookHotelRoomAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://hotelflightapi.onrender.com/api/HotelRoom/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while booking the hotel room with ID {id}.");
                throw;
            }
        }
    }
}
