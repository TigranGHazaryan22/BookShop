using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly BookShopContext _context;

        public AccountController(SignInManager<User> signInManager, BookShopContext context, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
        public async Task<IActionResult> LogoutConfirmed()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var orders = await _context.Orders
                    .Include(u => u.User)
                    .Where(o => o.User.Id == user.Id)
                    .ToListAsync();
                var reviews = await _context.Review
                    .Include(u => u.User)
                    .Where(r => r.User.Id == user.Id)
                    .ToListAsync();
                var polls = await _context.VoteAwards
                    .Include(u => u.Creator)
                    .Include(o => o.Votes)
                    .Where(r => r.Creator.Id == user.Id)
                    .ToListAsync();

                List<VoteOption> options = new List<VoteOption>();

                foreach(var o in polls)
                {
                    options.AddRange(o.Votes);
                }

                _context.Review.RemoveRange(reviews);
                _context.Orders.RemoveRange(orders);
                _context.VoteAwards.RemoveRange(polls);
                _context.Options.RemoveRange(options);

                await _signInManager.SignOutAsync();
                _context.User.Remove(user);

                await _context.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("", "Your account was not removed");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveConfirmed()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MyProfile(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
