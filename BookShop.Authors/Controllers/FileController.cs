using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Authors.Controllers
{
    public class FileController : Controller
    {
        private readonly BookShopContext _context;

        public FileController(BookShopContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> DownloadFile(int? id)
        {
            var file = await _context.File.FirstOrDefaultAsync(p => p.Id == id);

            if (file == null) return NotFound();

            return File(file.filling, file.type, file.name);
        }
    }
}
