using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookShop.Authors.Controllers
{
    public class ReviewController : Controller
    {
        private readonly BookShopContext _context;
        private readonly UserManager<User> _userManager;
        public ReviewController(BookShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddReview(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([Bind("Title,Stars,Description,Id")] Review review, int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            var book = await _context.Book.FindAsync(bookId);

            if (user == null) return Unauthorized();
            if (book == null) return BadRequest();

            if (review.Stars < 0)
            {
                review.Stars = 1;
            }
            else if (review.Stars > 5)
            {
                review.Stars = 5;
            }
            var _review = new Review
            {
                Title = review.Title,
                Stars = review.Stars,
                Description = review.Description,
                User = user,
                Book = book
            };

            _context.Review.Add(_review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Books", new { id = bookId });
        }
    }
}
