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
            var tickets = await _flightTicketService.GetAllFlightTicketsAsync();
            return View(tickets);
        }

        // Display details of a flight ticket for purchase
        public async Task<IActionResult> Buy(int id)
        {
            var ticket = await _flightTicketService.GetFlightTicketAsync(id);
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
            var ticket = await _flightTicketService.GetFlightTicketAsync(id);
            if (ticket != null)
            {
                // Placeholder for handling purchase, integrating with Cart, etc.
            }

            // Redirect to HotelRoom purchase after FlightTicket
            return RedirectToAction("MakePayment");
        }

        // Handle the Making of payments
        public IActionResult MakePayment()
        {
            return View();
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
                var createdTicket = await _flightTicketService.CreateFlightTicketAsync(flightTicket);
                if (createdTicket != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(flightTicket);
        }

        // Edit FlightTicket view
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _flightTicketService.GetFlightTicketAsync(id);
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
                await _flightTicketService.UpdateFlightTicketAsync(flightTicket);
                return RedirectToAction(nameof(Index));
            }
            return View(flightTicket);
        }

        // Delete FlightTicket view
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _flightTicketService.GetFlightTicketAsync(id);
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
            await _flightTicketService.DeleteFlightTicketAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}




















//using HotelFlightMVC.Models;
//using HotelFlightMVC.Services;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace HotelFlightMVC.Controllers
//{
//    public class FlightTicketController : Controller
//    {
//        private readonly FlightTicketService _flightTicketService;

//        public FlightTicketController(FlightTicketService flightTicketService)
//        {
//            _flightTicketService = flightTicketService;
//        }

//        // Get all available flight tickets
//        public async Task<IActionResult> Index()
//        {
//            var tickets = await _flightTicketService.GetAllFlightTickets();
//            return View(tickets);
//        }

//        // Display details of a flight ticket for purchase
//        public async Task<IActionResult> Buy(int id)
//        {
//            var ticket = await _flightTicketService.GetFlightTicket(id);
//            if (ticket == null)
//            {
//                return NotFound();
//            }
//            return View(ticket);
//        }

//        // Handle flight ticket purchase confirmation
//        [HttpPost]
//        public async Task<IActionResult> BuyConfirmed(int id)
//        {
//            var ticket = await _flightTicketService.GetFlightTicket(id);
//            if (ticket != null)
//            {
//                // Logic for handling purchase (can be saved in Cart)
//                // Placeholder for now, as Cart integration will follow
//            }

//            // Redirect to HotelRoom purchase after FlightTicket
//            return RedirectToAction("MakePayment");
//        }


//        // Handle the Making of payments 

//        public IActionResult MakePayment() {
//            return View();
//        }

//        // Create FlightTicket view
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // Create FlightTicket post
//        [HttpPost]
//        public async Task<IActionResult> Create(FlightTicket flightTicket)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _flightTicketService.CreateFlightTicket(flightTicket);
//                if (result)
//                {
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            return View(flightTicket);
//        }

//        // Edit FlightTicket view
//        public async Task<IActionResult> Edit(int id)
//        {
//            var ticket = await _flightTicketService.GetFlightTicket(id);
//            if (ticket == null)
//            {
//                return NotFound();
//            }
//            return View(ticket);
//        }

//        // Edit FlightTicket post
//        [HttpPost]
//        public async Task<IActionResult> Edit(FlightTicket flightTicket)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _flightTicketService.UpdateFlightTicket(flightTicket);
//                if (result)
//                {
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            return View(flightTicket);
//        }

//        // Delete FlightTicket view
//        public async Task<IActionResult> Delete(int id)
//        {
//            var ticket = await _flightTicketService.GetFlightTicket(id);
//            if (ticket == null)
//            {
//                return NotFound();
//            }
//            return View(ticket);
//        }

//        // Confirm deletion of FlightTicket
//        [HttpPost, ActionName("Delete")]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var result = await _flightTicketService.DeleteFlightTicket(id);
//            if (result)
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            return View();
//        }
//    }
//}










































//using HotelFlightMVC.Models;
//using HotelFlightMVC.Services;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace HotelFlightMVC.Controllers
//{
//    public class FlightTicketController : Controller
//    {
//        private readonly FlightTicketService _flightTicketService;

//        public FlightTicketController(FlightTicketService flightTicketService)
//        {
//            _flightTicketService = flightTicketService;
//        }

//        // Get all available flight tickets
//        public async Task<IActionResult> Index()
//        {
//            var tickets = await _flightTicketService.GetAllFlightTickets();
//            return View(tickets);
//        }

//        // Display details of a flight ticket for purchase
//        public async Task<IActionResult> Buy(int id)
//        {
//            var ticket = await _flightTicketService.GetFlightTicket(id);
//            if (ticket == null)
//            {
//                return NotFound();
//            }
//            return View(ticket);
//        }

//        // Handle flight ticket purchase confirmation
//        [HttpPost]
//        public async Task<IActionResult> BuyConfirmed(int id)
//        {
//            var ticket = await _flightTicketService.GetFlightTicket(id);
//            if (ticket != null)
//            {
//                // Logic for handling flight ticket purchase can go here (e.g., saving to Cart)
//                // Placeholder for now, as Cart integration will follow
//            }

//            // Redirect to the HotelRoom purchase page (HotelRoomController's Index action)
//            return RedirectToAction("Index", "HotelRoom");
//        }




//        // Create FlightTicket view
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // Create FlightTicket post
//        [HttpPost]
//        public async Task<IActionResult> Create(FlightTicket flightTicket)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _flightTicketService.CreateFlightTicket(flightTicket);
//                if (result)
//                {
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            return View(flightTicket);
//        }

//        // Edit FlightTicket view
//        public async Task<IActionResult> Edit(int id)
//        {
//            var ticket = await _flightTicketService.GetFlightTicket(id);
//            if (ticket == null)
//            {
//                return NotFound();
//            }
//            return View(ticket);
//        }

//        // Edit FlightTicket post
//        [HttpPost]
//        public async Task<IActionResult> Edit(FlightTicket flightTicket)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _flightTicketService.UpdateFlightTicket(flightTicket);
//                if (result)
//                {
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            return View(flightTicket);
//        }

//        // Delete FlightTicket view
//        public async Task<IActionResult> Delete(int id)
//        {
//            var ticket = await _flightTicketService.GetFlightTicket(id);
//            if (ticket == null)
//            {
//                return NotFound();
//            }
//            return View(ticket);
//        }

//        // Confirm deletion of FlightTicket
//        [HttpPost, ActionName("Delete")]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var result = await _flightTicketService.DeleteFlightTicket(id);
//            if (result)
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            return View();
//        }
//    }
//}








////using HotelFlightMVC.Models;
////using HotelFlightMVC.Services;
////using Microsoft.AspNetCore.Mvc;
////using System.Security.Claims;
////using System.Threading.Tasks;

////namespace HotelFlightMVC.Controllers
////{
////    public class FlightTicketController : Controller
////    {
////        private readonly FlightTicketService _flightTicketService;
////        private readonly CartService _cartService;

////        public FlightTicketController(FlightTicketService flightTicketService, CartService cartService)
////        {
////            _flightTicketService = flightTicketService;
////            _cartService = cartService;
////        }

////        // Get all available flight tickets
////        public async Task<IActionResult> Index()
////        {
////            var tickets = await _flightTicketService.GetAllFlightTickets();
////            return View(tickets);
////        }

////        // Display details of a flight ticket for purchase
////        public async Task<IActionResult> Buy(int id)
////        {
////            var ticket = await _flightTicketService.GetFlightTicket(id);
////            if (ticket == null)
////            {
////                return NotFound();
////            }
////            return View(ticket);
////        }

////        // Handle flight ticket purchase confirmation
////        [HttpPost]
////        public async Task<IActionResult> BuyConfirmed(int id)
////        {
////            var ticket = await _flightTicketService.GetFlightTicket(id);
////            if (ticket != null)
////            {
////                // Get the current user's ID
////                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Adjust this based on how you manage user authentication

////                // Logic for adding the ticket to the cart
////                var result = await _cartService.AddTicketToCart(userId, ticket); // Make sure this method exists in your CartService
////                if (!result)
////                {
////                    ModelState.AddModelError("", "Unable to add ticket to cart.");
////                    return View(ticket); // Optionally return to the Buy view
////                }
////            }

////            // Redirect to the HotelRoom purchase page
////            return RedirectToAction("Index", "HotelRoom");
////        }

////        // Create FlightTicket view
////        public IActionResult Create()
////        {
////            return View();
////        }

////        // Create FlightTicket post
////        [HttpPost]
////        public async Task<IActionResult> Create(FlightTicket flightTicket)
////        {
////            if (ModelState.IsValid)
////            {
////                var result = await _flightTicketService.CreateFlightTicket(flightTicket);
////                if (result)
////                {
////                    return RedirectToAction(nameof(Index));
////                }
////            }
////            return View(flightTicket);
////        }

////        // Edit FlightTicket view
////        public async Task<IActionResult> Edit(int id)
////        {
////            var ticket = await _flightTicketService.GetFlightTicket(id);
////            if (ticket == null)
////            {
////                return NotFound();
////            }
////            return View(ticket);
////        }

////        // Edit FlightTicket post
////        [HttpPost]
////        public async Task<IActionResult> Edit(FlightTicket flightTicket)
////        {
////            if (ModelState.IsValid)
////            {
////                var result = await _flightTicketService.UpdateFlightTicket(flightTicket);
////                if (result)
////                {
////                    return RedirectToAction(nameof(Index));
////                }
////            }
////            return View(flightTicket);
////        }

////        // Delete FlightTicket view
////        public async Task<IActionResult> Delete(int id)
////        {
////            var ticket = await _flightTicketService.GetFlightTicket(id);
////            if (ticket == null)
////            {
////                return NotFound();
////            }
////            return View(ticket);
////        }

////        // Confirm deletion of FlightTicket
////        [HttpPost, ActionName("Delete")]
////        public async Task<IActionResult> DeleteConfirmed(int id)
////        {
////            var result = await _flightTicketService.DeleteFlightTicket(id);
////            if (result)
////            {
////                return RedirectToAction(nameof(Index));
////            }
////            return View();
////        }
////    }
////}
