using BookShop.Data;
using BookShop.Helpers;
using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly BookShopContext _context;
        private readonly UserManager<User> _userManager;
        private readonly string SessionKey = "OrderSession";

        public OrderController(BookShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PlaceOrder()
        {
            var user = _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var orderBooks = HttpContext.Session.GetObject<List<Book>>(SessionKey);
            if (orderBooks.Any()) return BadRequest("Your order is empty");

            Order newOrder = new Order
            {
                User = (User)await user,
                Books = orderBooks,
                Date = DateTime.Now
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
