using HotelFlightMVC.Models;
using HotelFlightMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelFlightMVC.Controllers
{
    public class FlightTicketController : Controller
    {
        private readonly FlightTicketService _flightTicketService;

        public FlightTicketController(FlightTicketService flightTicketService)
        {
            _flightTicketService = flightTicketService;
        }

        // Get all available flight tickets
        public async Task<IActionResult> Index()
        {
            var tickets = await _flightTicketService.GetAllFlightTickets();
            return View(tickets);
        }

        // Display details of a flight ticket for purchase
        public async Task<IActionResult> Buy(int id)
        {
            var ticket = await _flightTicketService.GetFlightTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // Handle flight ticket purchase confirmation
        [HttpPost]
        public async Task<IActionResult> BuyConfirmed(int id)
        {
            var ticket = await _flightTicketService.GetFlightTicket(id);
            if (ticket != null)
            {
                // Logic for handling purchase (can be saved in Cart)
                // Placeholder for now, as Cart integration will follow
            }

            // Redirect to HotelRoom purchase after FlightTicket
            return RedirectToAction("BuyHotelRoom", "HotelRoom");
        }

        // Create FlightTicket view
        public IActionResult Create()
        {
            return View();
        }

        // Create FlightTicket post
        [HttpPost]
        public async Task<IActionResult> Create(FlightTicket flightTicket)
        {
            if (ModelState.IsValid)
            {
                var result = await _flightTicketService.CreateFlightTicket(flightTicket);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(flightTicket);
        }

        // Edit FlightTicket view
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _flightTicketService.GetFlightTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // Edit FlightTicket post
        [HttpPost]
        public async Task<IActionResult> Edit(FlightTicket flightTicket)
        {
            if (ModelState.IsValid)
            {
                var result = await _flightTicketService.UpdateFlightTicket(flightTicket);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(flightTicket);
        }

        // Delete FlightTicket view
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _flightTicketService.GetFlightTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // Confirm deletion of FlightTicket
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _flightTicketService.DeleteFlightTicket(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
