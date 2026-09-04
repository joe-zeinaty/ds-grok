using Microsoft.AspNetCore.Mvc;
using D_S_Grok.Data;

namespace D_S_Grok.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var users = _context.Users
                .OrderByDescending(u => u.TotalXP)
                .ThenBy(u => u.CreatedAt)
                .Take(10)
                .ToList();

            ViewBag.CurrentUserID = userId.Value;

            return View(users);
        }
    }
}