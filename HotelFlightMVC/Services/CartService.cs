using HotelFlightMVC.Models;
using Newtonsoft.Json;
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
                // Handle error
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
    }
}
