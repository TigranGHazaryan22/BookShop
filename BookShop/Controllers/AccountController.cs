using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
