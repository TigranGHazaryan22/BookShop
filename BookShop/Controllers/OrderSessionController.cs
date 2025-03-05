using BookShop.Data;
using BookShop.Helpers;
using BookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class OrderSessionController : Controller
    {
        private readonly BookShopContext _context;
        private readonly string SessionKey = "OrderSession";

        public OrderSessionController(BookShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var order = HttpContext.Session.GetObject<List<Book>>(SessionKey) ?? new List<Book>();

            Dictionary<int, int> counts = new Dictionary<int, int>();
            Dictionary<int, decimal> sums = new Dictionary<int, decimal>();
            List<Book> uniqueBooks = new List<Book>();

            foreach(var o in order)
            {
                if (counts.ContainsKey(o.Id))
                {
                    counts[o.Id] += 1;
                    sums[o.Id] += o.Price;
                }
                else
                {
                    counts.Add(o.Id, 1);
                    sums.Add(o.Id, o.Price);
                    uniqueBooks.Add(o);
                }
            }

            ViewBag.Sum = sums;
            ViewBag.Sums = sums.Values.Sum();
            ViewBag.Count = counts;
            return View(uniqueBooks);
        }

        [Authorize]
        public IActionResult AddToOrder(int id)
        {
            var book = _context.Book.Find(id);
            if (book == null) return NotFound();

            var order = HttpContext.Session.GetObject<List<Book>>(SessionKey) ?? new List<Book>();
            order.Add(book);

            HttpContext.Session.SetObject(SessionKey, order);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoveFromOrder(int id)
        {
            var order = HttpContext.Session.GetObject<List<Book>>(SessionKey) ?? new List<Book>();
            var bookToRemove = order.FirstOrDefault(b => b.Id == id);

            if (bookToRemove != null)
            {
                order.Remove(bookToRemove);
                HttpContext.Session.SetObject(SessionKey, order);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult ClearOrder()
        {
            HttpContext.Session.Remove(SessionKey);
            return RedirectToAction("Index");
        }
    }
}
