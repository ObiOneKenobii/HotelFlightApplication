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

       
        public async Task<IEnumerable<FlightTicket>> GetBeveragesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<FlightTicket>>("https://hotelflightapi.onrender.com/api/FlightTicket");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching beverages.");
                throw;
            }
        }

        public async Task<FlightTicket> GetBeverageAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<FlightTicket>($"https://hotelflightapi.onrender.com/api/FlightTicket/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the beverage with ID {id}.");
                throw;
            }
        }






        public async Task<FlightTicket> CreateFlightTicketAsync(FlightTicket flightTicket)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/FlightTicket", flightTicket);
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
                var response = await _httpClient.PutAsJsonAsync($"api/FlightTicket/{flightTicket.Id}", flightTicket);
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
                var response = await _httpClient.DeleteAsync($"api/FlightTicket/{id}");
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
