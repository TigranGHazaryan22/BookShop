using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookShop.Controllers
{
    public class VoteAwardController : Controller
    {
        private readonly BookShopContext _context;
        private readonly UserManager<User> _userManager;

        public VoteAwardController(BookShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.VoteAwards.ToListAsync());
        }

        public async Task<IActionResult> VoteView(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            ViewBag.Id = id;

            var poll = await _context.VoteAwards
                .Include(v => v.Votes).ThenInclude(o => o.Author)
                .Include(o => o.VotedUsers)
                .Include(a => a.Funders)
                .FirstOrDefaultAsync(p => p.Id == id);

            return View(poll);
        }

        public async Task<IActionResult> AddAward()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            List<Author> authors = await _context.Author.ToListAsync();
            ViewData["Authors"] = authors;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAward([Bind("Title,Stars,Description,Id")] VoteAward voteAward, List<string> authorIds, string end)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            authorIds = authorIds[0].Split(',').ToList();

            var authors = await _context.Author
                .Where(a => authorIds.Contains(a.Id))
                .ToListAsync();
            if (ModelState.IsValid)
            {
                var _award = new VoteAward
                {
                    Title = voteAward.Title,
                    Description = voteAward.Description,
                    Funders = new List<User> { user },
                    Date = DateTime.Now,
                    End = DateTime.Parse(end),
                    Creator = user
                };

                foreach (var author in authors)
                {
                    _award.Votes.Add(new VoteOption
                    {
                        Counts = 0,
                        Author = author
                    });
                }
                bool b = false;

                if (_award.Votes.Count < 2)
                {
                    ModelState.AddModelError("", "Add more authors");
                    b = true;
                }
                if (_award.Date > _award.End)
                {
                    ModelState.AddModelError("", "Invalid duration for the vote");
                    b = true;
                }

                if (b)
                {
                    return View();
                }
                _context.VoteAwards.Add(_award);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                ViewData["Authors"] = await _context.Author.ToListAsync();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVote(int pollId, string selectedAuthor)
        {
            var votingPoll = await _context.VoteAwards
                .Include(v => v.Votes).ThenInclude(o => o.Author)
                .Include(v => v.VotedUsers)
                .FirstOrDefaultAsync(v => v.Id == pollId);
            var user = await _userManager.GetUserAsync(User);

            if (votingPoll == null) return NotFound();
            if (user == null) return Unauthorized();
            if (votingPoll.VotedUsers.Contains(user))
            {
                TempData["Message"] = "You have already voted for this award.";
                return RedirectToAction("VoteView", new { id = pollId });
            }

            foreach (VoteOption option in votingPoll.Votes)
            {
                if (option.Author.Id == selectedAuthor)
                {
                    option.Counts++;
                    votingPoll.VotedUsers.Add(user);
                    break;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BecomeFunder(int pollId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var funders = await _context.VoteAwards
                .Include(a => a.Funders)
                .FirstOrDefaultAsync(a => a.Id == pollId);

            if (!funders.Funders.Contains(user))
            {
                funders.Funders.Add(user);
            }
            else
            {
                ModelState.AddModelError("", "You are already a funder for this poll");
            }

            await _context.SaveChangesAsync();

            var thePoll = await _context.VoteAwards
                .Include(a => a.Votes).ThenInclude(o => o.Author)
                .Include(u => u.VotedUsers)
                .Include(u => u.Funders)
                .FirstOrDefaultAsync(a => pollId == a.Id);
            return View(thePoll);
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
