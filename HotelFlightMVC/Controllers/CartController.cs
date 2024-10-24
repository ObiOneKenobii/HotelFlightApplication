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
                var cart = await _cartService.GetCart(userId);

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
                var cart = await _cartService.GetCart(userId);

                if (cart == null || (cart.NumberOfTickets == 0 && cart.NumberOfRooms == 0))
                {
                    ModelState.AddModelError("", "Your cart is empty. Please add items before checkout.");
                    return RedirectToAction("Index");
                }

                await _cartService.DeleteCart(userId); // Clear the cart after checkout
                return RedirectToAction("PaymentSuccess");
            }

            public IActionResult PaymentSuccess()
            {
                return View();
            }
        }
    }










//public class CartController : Controller
//{
//    private readonly ApplicationDbContext _context;

//    public CartController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    // Display the cart with flight tickets and hotel rooms
//    public IActionResult Index()
//    {
//        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user's ID

//        if (string.IsNullOrEmpty(userId))
//        {
//            // If the user is not logged in, redirect them to the login page
//            return RedirectToAction("Login", "Account");
//        }

//        // Fetch the cart for the logged-in user
//        var cart = _context.Carts
//                           .FirstOrDefault(c => c.UserId == userId); // Ensure UserId is a string here

//        if (cart == null)
//        {
//            // If the cart doesn't exist, create a new cart
//            cart = new Cart
//            {
//                UserId = userId,
//                FlightTicket = null,
//                HotelRoom = null,
//                NumberOfTickets = 0,
//                NumberOfRooms = 0
//            };
//            _context.Carts.Add(cart);
//            _context.SaveChanges();
//        }

//        return View(cart); // Pass the cart to the view
//    }

//    // Confirm and proceed to the payment success page
//    [HttpPost]
//    public IActionResult Checkout()
//    {
//        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user's ID

//        var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId); // Ensure UserId is a string

//        if (cart == null || (cart.NumberOfTickets == 0 && cart.NumberOfRooms == 0))
//        {
//            // If the cart is empty, display an error message
//            ModelState.AddModelError("", "Your cart is empty. Please add items before checkout.");
//            return RedirectToAction("Index");
//        }

//        // Finalize the checkout process here
//        // (e.g., you might want to create an order, process payment, etc.)

//        // Clear or remove the cart after successful checkout
//        _context.Carts.Remove(cart);
//        _context.SaveChanges();

//        return RedirectToAction("PaymentSuccess");
//    }

//    public IActionResult PaymentSuccess()
//    {
//        return View();
//    }
//}