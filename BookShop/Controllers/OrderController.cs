using BookShop.Data;
using BookShop.Helpers;
using BookShop.Migrations;
using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> PlaceOrder([FromBody] Dictionary<int, int> counts)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            List<OrderBooks> orderBooks = new List<OrderBooks>();

            Order newOrder = new Order
            {
                User = user,
                Date = DateTime.Now
            };

            foreach (var book in counts.Keys)
            {
                orderBooks.Add(new OrderBooks
                {
                    Order = newOrder,
                    Book = await _context.Book.FirstOrDefaultAsync(a => a.Id == book),
                    Count = counts[book]
                });
            }

            newOrder.Counts = orderBooks;

            _context.Add(newOrder);
            _context.OrderBook.AddRange(orderBooks);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var orders = await _context.Orders
                .Where(a => a.User.Id == user.Id)
                .Include(o => o.Counts).ThenInclude(o => o.Book)
                .ToListAsync();

            return View(orders);
        }
    }
}
