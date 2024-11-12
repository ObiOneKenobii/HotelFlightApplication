using HotelFlightMVC.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HotelFlightMVC.Services
{
    public class FlightTicketService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FlightTicketService> _logger;

        public FlightTicketService(HttpClient httpClient, ILogger<FlightTicketService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<FlightTicket>> GetAllFlightTicketsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<FlightTicket>>("https://hotelflightapi.onrender.com");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching flight tickets.");
                throw;
            }
        }

        public async Task<FlightTicket> GetFlightTicketAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<FlightTicket>($"https://hotelflightapi.onrender.com/FlightTicket/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the flight ticket with ID {id}.");
                throw;
            }
        }

        public async Task<FlightTicket> CreateFlightTicketAsync(FlightTicket flightTicket)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://hotelflightapi.onrender.com/api/FlightTicket", flightTicket);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<FlightTicket>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new flight ticket.");
                throw;
            }
        }

        public async Task UpdateFlightTicketAsync(FlightTicket flightTicket)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://hotelflightapi.onrender.com/api/FlightTicket/{flightTicket.Id}", flightTicket);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the flight ticket with ID {flightTicket.Id}.");
                throw;
            }
        }

        public async Task DeleteFlightTicketAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://hotelflightapi.onrender.com/api/FlightTicket/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the flight ticket with ID {id}.");
                throw;
            }
        }
    }
}
















//using HotelFlightMVC.Models;
//using Newtonsoft.Json;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//namespace HotelFlightMVC.Services
//{
//    public class FlightTicketService
//    {
//        private readonly HttpClient _httpClient;
//        private readonly CartService _cartService;

//        public FlightTicketService(HttpClient httpClient, CartService cartService)
//        {
//            _httpClient = httpClient;
//            _cartService = cartService;
//        }

//        public async Task<IEnumerable<FlightTicket>> GetAllFlightTickets()
//        {
//            var response = await _httpClient.GetAsync("api/FlightTicket");
//            if (response.IsSuccessStatusCode)
//            {
//                var jsonData = await response.Content.ReadAsStringAsync();
//                return JsonConvert.DeserializeObject<IEnumerable<FlightTicket>>(jsonData);
//            }
//            return null;
//        }

//        public async Task<FlightTicket> GetFlightTicket(int id)
//        {
//            var response = await _httpClient.GetAsync($"api/FlightTicket/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var jsonData = await response.Content.ReadAsStringAsync();
//                return JsonConvert.DeserializeObject<FlightTicket>(jsonData);
//            }
//            return null;
//        }

//        public async Task<bool> CreateFlightTicket(FlightTicket flightTicket)
//        {
//            var jsonData = JsonConvert.SerializeObject(flightTicket);
//            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
//            var response = await _httpClient.PostAsync("api/FlightTicket", content);
//            return response.IsSuccessStatusCode;
//        }

//        public async Task<bool> UpdateFlightTicket(FlightTicket flightTicket)
//        {
//            var jsonData = JsonConvert.SerializeObject(flightTicket);
//            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
//            var response = await _httpClient.PutAsync($"api/FlightTicket/{flightTicket.Id}", content);
//            return response.IsSuccessStatusCode;
//        }

//        public async Task<bool> DeleteFlightTicket(int id)
//        {
//            var response = await _httpClient.DeleteAsync($"api/FlightTicket/{id}");
//            return response.IsSuccessStatusCode;
//        }

//        // Method to add a flight ticket to the user's cart
//        public async Task<bool> AddTicketToCart(string userId, FlightTicket flightTicket)
//        {
//            // Use the CartService to add the ticket to the cart
//            return await _cartService.AddTicketToCart(userId, flightTicket);
//        }
//    }
//}
