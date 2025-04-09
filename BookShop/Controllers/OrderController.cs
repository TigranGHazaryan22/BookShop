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

        public async Task<IActionResult> PlaceOrder()
        {
            var order = HttpContext.Session.GetObject<List<Book>>(SessionKey) ?? new List<Book>();
            var user = await _userManager.GetUserAsync(User);

            if (order == null)
                return BadRequest("Your basket is empty");

            if (user == null)
                return Unauthorized();

            Dictionary<int, int> counts = new Dictionary<int, int>();

            foreach (var o in order)
            {
                if (counts.Keys.Contains(o.Id))
                    counts[o.Id]++;
                else
                    counts.Add(o.Id, 1);
            }

            Order newOrder = new Order()
            {
                User = user,
                Date = DateTime.Now
            };

            List<OrderBooks> orderBooks = new List<OrderBooks>();

            foreach (int id in counts.Keys)
            {
                var book = await _context.Book.FirstOrDefaultAsync(b => b.Id == id);

                if (book != null)
                {
                    orderBooks.Add(new OrderBooks()
                    {
                        Book = book,
                        Count = counts[id],
                        Order = newOrder
                    });
                }

            }

            newOrder.Counts = orderBooks;

            _context.Orders.Add(newOrder);
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
