 using global::HotelFlightMVC.Services;
using HotelFlightMVC.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    namespace HotelFlightMVC.Controllers
    {
        public class CartController : Controller
        {
            private readonly CartService _cartService;

            public CartController(CartService cartService)
            {
                _cartService = cartService;
            }

            public async Task<IActionResult> Index()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var cart = await _cartService.GetCartAsync(userId);

                if (cart == null)
                {
                    // Handle no cart case
                    return View(new Cart());
                }

                return View(cart);
            }

            [HttpPost]
            public async Task<IActionResult> Checkout()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var cart = await _cartService.GetCartAsync(userId);

                if (cart == null || (cart.NumberOfTickets == 0 && cart.NumberOfRooms == 0))
                {
                    ModelState.AddModelError("", "Your cart is empty. Please add items before checkout.");
                    return RedirectToAction("Index");
                }

                await _cartService.DeleteCartAsync(userId); // Clear the cart after checkout
                return RedirectToAction("PaymentSuccess");
            }

            public IActionResult PaymentSuccess()
            {
                return View();
            }
        }
    }




