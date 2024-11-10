using HotelFlightMVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelFlightMVC.Services
{
    public class CartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Cart> GetCart(string userId)
        {
            var response = await _httpClient.GetAsync($"api/Cart/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Cart>(jsonData);
            }
            else
            {
                // Log error or throw an exception based on your logging strategy
                return null;
            }
        }

        public async Task<bool> DeleteCart(string userId)
        {
            var response = await _httpClient.DeleteAsync($"api/Cart/{userId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateCart(Cart cart)
        {
            var jsonData = JsonConvert.SerializeObject(cart);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Cart", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCart(Cart cart)
        {
            var jsonData = JsonConvert.SerializeObject(cart);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Cart/{cart.UserId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddTicketToCart(string userId, FlightTicket ticket)
        {
            // Get the user's cart
            var cart = await GetCart(userId) ?? new Cart { UserId = userId, FlightTickets = new List<FlightTicket>() };

            // Add the ticket to the cart
            cart.FlightTickets.Add(ticket);

            // Update the cart
            return await UpdateCart(cart);
        }

        public async Task<bool> ViewCart(string userId)
        {
            var cart = await GetCart(userId);
            if (cart == null)
            {
                // Handle case when cart does not exist
                return false;
            }
            // Return cart or process it as required
            return true;
        }
    }
}
