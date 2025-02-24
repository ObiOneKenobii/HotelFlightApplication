﻿using HotelFlightMVC.Models;
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
            var hotelRooms = await _hotelRoomService.GetAllHotelRoomsAsync();
            return View(hotelRooms);
        }

        // Display details of a hotel room for booking (equivalent to "Buy")
        public async Task<IActionResult> Buy(int id)
        {
            var hotelRoom = await _hotelRoomService.GetHotelRoomAsync(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }
            return View(hotelRoom);
        }

        // Handle hotel room booking confirmation (equivalent to "BuyConfirmed")
        [HttpPost]
        public async Task<IActionResult> BuyConfirmed(int id)
        {
            var hotelRoom = await _hotelRoomService.GetHotelRoomAsync(id);
            if (hotelRoom != null)
            {
                // Logic for handling booking (can be added to Cart)
            }

            // Redirect to the Cart after booking
            return RedirectToAction("MakePayment");
        }

        // Display MakePayment page after booking is confirmed
        public IActionResult MakePayment()
        {
            return View();
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
                var result = await _hotelRoomService.CreateHotelRoomAsync(hotelRoom);
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
            var hotelRoom = await _hotelRoomService.GetHotelRoomAsync(id);
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
                var result = await _hotelRoomService.UpdateHotelRoomAsync(hotelRoom);
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
            var hotelRoom = await _hotelRoomService.GetHotelRoomAsync(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }
            return View(hotelRoom);
        }

        // Confirm deletion of HotelRoom
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _hotelRoomService.DeleteHotelRoomAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
