using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookShopContext _context;
        private readonly UserManager<Author> _userManager;

        public BooksController(BookShopContext context, UserManager<Author> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.file)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            List<Review> reviews = await _context.Review
                .Where(r => r.Book.Id == id)
                .Include(r => r.User)
                .ToListAsync();

            ViewData["Reviews"] = reviews;
            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            List<Author> authors = await _context.Author.ToListAsync();
            ViewData["Authors"] = authors;
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Genre,Price,IsElectronicAvailable,IsAvailable,AgeRestriction")] Book book, List<string> AuthorIds, IFormFile file)
        {
            ModelState.Remove("file");
            if (ModelState.IsValid)
            {
                List<Author> authors = await _context.Author.Where(a => AuthorIds.Contains(a.Id)).ToListAsync();
                book.Authors = authors;
                book.file = new Models.File();

                if (file != null && file.Length > 0)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        book.file.name = file.FileName;
                        book.file.type = file.ContentType;
                        book.file.filling = memoryStream.ToArray();
                    }
                    book.file.Date = DateTime.Now;
                }
                else
                {
                    book.file = null;
                    book.IsElectronicAvailable = false;
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Authors"] = await _context.Author.ToListAsync();
            return View(book);
        }



        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Genre,Price,IsElectronicAvailable,IsAvailable,AgeRestriction")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            foreach (var o in book.Authors)
            {
                if (ModelState.IsValid && user.Id == o.Id)
                {
                    try
                    {
                        _context.Update(book);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BookExists(book.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAuthors(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Json(new List<object>());

            var authors = await _context.Author
                .Where(a => a.FirstName.Contains(query) || a.LastName.Contains(query))
                .Select(a => new { a.Id, a.FirstName, a.LastName, a.Email })
                .ToListAsync();

            return Json(authors);
        }
    }
}