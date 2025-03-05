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

            if(user != null)
            {
                var orders = await _context.Orders.ToListAsync();

                foreach(var o in orders)
                {
                    if (o.User.Id == user.Id)
                        orders.Remove(o);
                }

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
    }
}
