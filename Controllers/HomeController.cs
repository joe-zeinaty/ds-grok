using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using D_S_Grok.Data;
using D_S_Grok.Models;

namespace D_S_Grok.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
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

            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }

            int totalLessons = _context.Lessons.Count();

            int completedLessons = _context.LessonProgress
                .Count(lp => lp.UserID == user.UserID && lp.IsCompleted);

            int totalChallenges = _context.Challenges.Count();

            int completedChallenges = _context.ChallengeAttempts
                .Count(ca => ca.UserID == user.UserID && ca.IsCompleted);

            int totalQuizzes = _context.Quizzes.Count();

            int completedQuizzes = _context.QuizAttempts
    .Where(qa => qa.UserID == user.UserID && qa.IsPassed)
    .Select(qa => qa.QuizID)
    .Distinct()
    .Count();

            int totalActivities = totalLessons + totalChallenges + totalQuizzes;

            int completedActivities = completedLessons + completedChallenges + completedQuizzes;

            int progressPercentage = totalActivities == 0
                ? 0
                : (int)Math.Round((completedActivities / (double)totalActivities) * 100);

            int achievementCount = _context.UserAchievements
                .Count(ua => ua.UserID == user.UserID);

            var achievements = _context.Achievements
                .OrderBy(a => a.AchievementID)
                .ToList();

            var unlockedAchievementIds = _context.UserAchievements
                .Where(ua => ua.UserID == user.UserID)
                .Select(ua => ua.AchievementID)
                .ToList();

            var recentAchievements = _context.UserAchievements
                .Where(ua => ua.UserID == user.UserID)
                .Include(ua => ua.Achievement)
                .OrderByDescending(ua => ua.EarnedAt)
                .Take(3)
                .ToList();

            int dailyStreak = CalculateDailyStreak(user.UserID);
            HttpContext.Session.SetInt32("DailyStreak", dailyStreak);

            ViewBag.DailyStreak = dailyStreak;
            ViewBag.Username = user.Username;
            ViewBag.TotalXP = user.TotalXP;
            ViewBag.CurrentLevel = user.CurrentLevel;

            ViewBag.CompletedLessons = completedLessons;
            ViewBag.TotalLessons = totalLessons;

            ViewBag.CompletedChallenges = completedChallenges;
            ViewBag.TotalChallenges = totalChallenges;

            ViewBag.CompletedQuizzes = completedQuizzes;
            ViewBag.TotalQuizzes = totalQuizzes;

            ViewBag.CompletedActivities = completedActivities;
            ViewBag.TotalActivities = totalActivities;
            ViewBag.ProgressPercentage = progressPercentage;

            ViewBag.AchievementCount = achievementCount;
            ViewBag.Achievements = achievements;
            ViewBag.UnlockedAchievementIds = unlockedAchievementIds;
            ViewBag.RecentAchievements = recentAchievements;

            return View();
        }

        private int CalculateDailyStreak(int userId)
{
    var activityDates = _context.XPTransactions
        .Where(x => x.UserID == userId)
        .Select(x => x.CreatedAt.Date)
        .Distinct()
        .OrderByDescending(d => d)
        .ToList();

    if (!activityDates.Any())
        return 0;

    DateTime today = DateTime.Today;
    DateTime yesterday = today.AddDays(-1);

    if (activityDates[0] != today && activityDates[0] != yesterday)
        return 0;

    int streak = 0;
    DateTime currentDate = activityDates[0];

    foreach (var date in activityDates)
    {
        if (date == currentDate)
        {
            streak++;
            currentDate = currentDate.AddDays(-1);
        }
    }

    return streak;
}

        public IActionResult Privacy()
        {
            return View();
        }
    }
}