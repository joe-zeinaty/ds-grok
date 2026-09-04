using Microsoft.AspNetCore.Mvc;
using D_S_Grok.Data;
using D_S_Grok.Models;
using D_S_Grok.Services;


namespace D_S_Grok.Controllers
{
    public class ProgressController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AchievementService _achievementService;

        public ProgressController(
    ApplicationDbContext context,
    AchievementService achievementService)
{
    _context = context;
    _achievementService = achievementService;
}

        [HttpPost]
public IActionResult CompleteLesson(int lessonId)
{
    int? userId = HttpContext.Session.GetInt32("UserID");

    if (userId == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var lesson = _context.Lessons.FirstOrDefault(l => l.LessonID == lessonId);

    if (lesson == null)
    {
        return RedirectToAction("Index", "Lessons");
    }

    var existingProgress = _context.LessonProgress
        .FirstOrDefault(lp => lp.UserID == userId && lp.LessonID == lessonId);

    if (existingProgress != null)
{
    _achievementService.CheckAchievements(userId.Value);
    TempData["Success"] = "You already completed this lesson.";
    return RedirectToAction("Index", "Lessons");
}

    var user = _context.Users.FirstOrDefault(u => u.UserID == userId);

    if (user == null)
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }

    _context.LessonProgress.Add(new LessonProgress
    {
        UserID = user.UserID,
        LessonID = lesson.LessonID,
        IsCompleted = true,
        CompletedAt = DateTime.Now
    });

    _context.XPTransactions.Add(new XPTransaction
    {
        UserID = user.UserID,
        XPAmount = lesson.XPReward,
        SourceType = "Lesson",
        SourceReferenceID = lesson.LessonID,
        CreatedAt = DateTime.Now
    });

    user.TotalXP += lesson.XPReward;

    if (user.TotalXP >= 350)
        user.CurrentLevel = 3;
    else if (user.TotalXP >= 150)
        user.CurrentLevel = 2;
    else
        user.CurrentLevel = 1;

    _context.SaveChanges();

    _achievementService.CheckAchievements(user.UserID);

    TempData["Success"] = $"Lesson completed! +{lesson.XPReward} XP";

    return RedirectToAction("Index", "Lessons");
}
    }
}