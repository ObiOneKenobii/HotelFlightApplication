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
    public class CartService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CartService> _logger;

        public CartService(HttpClient httpClient, ILogger<CartService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Get a cart by user ID
        public async Task<Cart> GetCartAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://hotelflightapi.onrender.com/api/Cart/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Cart>(jsonData);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the cart for user ID {userId}.");
                throw;
            }
        }

        // Delete a cart by user ID
        public async Task<bool> DeleteCartAsync(string userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://hotelflightapi.onrender.com/api/Cart/{userId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the cart for user ID {userId}.");
                throw;
            }
        }

        // Create a new cart
        public async Task<bool> CreateCartAsync(Cart cart)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(cart);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://hotelflightapi.onrender.com/api/Cart", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new cart.");
                throw;
            }
        }

        // Update an existing cart
        public async Task<bool> UpdateCartAsync(Cart cart)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(cart);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://hotelflightapi.onrender.com/api/Cart/{cart.UserId}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the cart for user ID {cart.UserId}.");
                throw;
            }
        }

        // Add a flight ticket to the cart
        public async Task<bool> AddTicketToCartAsync(string userId, FlightTicket ticket)
        {
            try
            {
                // Get the user's cart, or create a new one if it doesn't exist
                var cart = await GetCartAsync(userId) ?? new Cart { UserId = userId, FlightTickets = new List<FlightTicket>() };

                // Add the ticket to the cart
                cart.FlightTickets.Add(ticket);

                // Update the cart
                return await UpdateCartAsync(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding a ticket to the cart for user ID {userId}.");
                throw;
            }
        }

        // View the cart by user ID (returns whether cart exists)
        public async Task<bool> ViewCartAsync(string userId)
        {
            try
            {
                var cart = await GetCartAsync(userId);
                if (cart == null)
                {
                    _logger.LogWarning($"No cart found for user ID {userId}.");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while viewing the cart for user ID {userId}.");
                throw;
            }
        }
    }
}
