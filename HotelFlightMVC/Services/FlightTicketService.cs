using HotelFlightMVC.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HotelFlightMVC.Services
{
    public class FlightTicketService
    {
        private readonly HttpClient _httpClient;
        private readonly CartService _cartService;

        public FlightTicketService(HttpClient httpClient, CartService cartService)
        {
            _httpClient = httpClient;
            _cartService = cartService;
        }

        public async Task<IEnumerable<FlightTicket>> GetAllFlightTickets()
        {
            var response = await _httpClient.GetAsync("api/FlightTicket");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<FlightTicket>>(jsonData);
            }
            return null;
        }

        public async Task<FlightTicket> GetFlightTicket(int id)
        {
            var response = await _httpClient.GetAsync($"api/FlightTicket/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<FlightTicket>(jsonData);
            }
            return null;
        }

        public async Task<bool> CreateFlightTicket(FlightTicket flightTicket)
        {
            var jsonData = JsonConvert.SerializeObject(flightTicket);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/FlightTicket", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateFlightTicket(FlightTicket flightTicket)
        {
            var jsonData = JsonConvert.SerializeObject(flightTicket);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/FlightTicket/{flightTicket.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFlightTicket(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/FlightTicket/{id}");
            return response.IsSuccessStatusCode;
        }

        // Method to add a flight ticket to the user's cart
        public async Task<bool> AddTicketToCart(string userId, FlightTicket flightTicket)
        {
            // Use the CartService to add the ticket to the cart
            return await _cartService.AddTicketToCart(userId, flightTicket);
        }
    }
}
