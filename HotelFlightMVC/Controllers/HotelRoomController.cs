using HotelFlightMVC.Models;
using HotelFlightMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelFlightMVC.Controllers
{
    public class HotelRoomController : Controller
    {
        private readonly HotelRoomService _hotelRoomService;

        public HotelRoomController(HotelRoomService hotelRoomService)
        {
            _hotelRoomService = hotelRoomService;
        }

        // Get all available hotel rooms
        public async Task<IActionResult> Index()
        {
            var hotelRooms = await _hotelRoomService.GetAllHotelRooms();
            return View(hotelRooms);
        }

        // Display details of a hotel room for booking
        public async Task<IActionResult> Book(int id)
        {
            var hotelRoom = await _hotelRoomService.GetHotelRoom(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }
            return View(hotelRoom);
        }

        // Handle hotel room booking confirmation
        [HttpPost]
        public async Task<IActionResult> BookConfirmed(int id)
        {
            var hotelRoom = await _hotelRoomService.GetHotelRoom(id);
            if (hotelRoom != null)
            {
                // Logic for booking (add to Cart)
            }

            // Redirect to the Cart after booking
            return RedirectToAction("Index", "Cart");
        }

        // Create HotelRoom view
        public IActionResult Create()
        {
            return View();
        }

        // Create HotelRoom post
        [HttpPost]
        public async Task<IActionResult> Create(HotelRoom hotelRoom)
        {
            if (ModelState.IsValid)
            {
                var result = await _hotelRoomService.CreateHotelRoom(hotelRoom);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(hotelRoom);
        }

        // Edit HotelRoom view
        public async Task<IActionResult> Edit(int id)
        {
            var hotelRoom = await _hotelRoomService.GetHotelRoom(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }
            return View(hotelRoom);
        }

        // Edit HotelRoom post
        [HttpPost]
        public async Task<IActionResult> Edit(HotelRoom hotelRoom)
        {
            if (ModelState.IsValid)
            {
                var result = await _hotelRoomService.UpdateHotelRoom(hotelRoom);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(hotelRoom);
        }

        // Delete HotelRoom view
        public async Task<IActionResult> Delete(int id)
        {
            var hotelRoom = await _hotelRoomService.GetHotelRoom(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }
            return View(hotelRoom);
        }

        // Confirm deletion of HotelRoom
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _hotelRoomService.DeleteHotelRoom(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
