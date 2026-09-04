using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using D_S_Grok.Data;

namespace D_S_Grok.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LessonsController(ApplicationDbContext context)
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

    var levels = _context.Levels
        .OrderBy(l => l.LevelNumber)
        .ToList();

    var lessons = _context.Lessons
        .OrderBy(l => l.LevelID)
        .ThenBy(l => l.LessonOrder)
        .ToList();

    var completedLessonIds = _context.LessonProgress
        .Where(lp => lp.UserID == userId.Value && lp.IsCompleted)
        .Select(lp => lp.LessonID)
        .ToList();

    ViewBag.Lessons = lessons;
    ViewBag.CompletedLessonIds = completedLessonIds;

    return View(levels);
}

        public IActionResult PointerBasics()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}