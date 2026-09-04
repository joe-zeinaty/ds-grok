using Microsoft.AspNetCore.Mvc;
using D_S_Grok.Data;
using D_S_Grok.Models;
using D_S_Grok.Services;

namespace D_S_Grok.Controllers
{
    public class ChallengesController : Controller
    {
        private readonly ApplicationDbContext _context;
private readonly AchievementService _achievementService;

public ChallengesController(
    ApplicationDbContext context,
    AchievementService achievementService)
{
    _context = context;
    _achievementService = achievementService;
}

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var challenges = _context.Challenges
                .OrderBy(c => c.LevelID)
                .ToList();

            return View(challenges);
        }

        public IActionResult LinkedListTraversal()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public IActionResult LinkedListOperations()
{
    int? userId = HttpContext.Session.GetInt32("UserID");

    if (userId == null)
    {
        return RedirectToAction("Login", "Account");
    }

    return View();
}

        [HttpPost]
public IActionResult CompleteChallenge(int challengeId)
{
    int? userId = HttpContext.Session.GetInt32("UserID");

    if (userId == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var challenge = _context.Challenges
        .FirstOrDefault(c => c.ChallengeID == challengeId);

    if (challenge == null)
    {
        return RedirectToAction("Index");
    }

    var existingAttempt = _context.ChallengeAttempts
        .FirstOrDefault(ca =>
            ca.UserID == userId.Value &&
            ca.ChallengeID == challengeId);

    if (existingAttempt != null)
    {
        TempData["Success"] = "You already completed this challenge.";
        if (challengeId == 2)
{
    return RedirectToAction("Index", "Challenges");
}

return RedirectToAction("Index", "Challenges");
    }

    var user = _context.Users.First(u => u.UserID == userId.Value);

    _context.ChallengeAttempts.Add(new ChallengeAttempt
    {
        UserID = user.UserID,
        ChallengeID = challenge.ChallengeID,
        IsCompleted = true,
        CompletedAt = DateTime.Now,
        AttemptsCount = 1
    });

    _context.XPTransactions.Add(new XPTransaction
    {
        UserID = user.UserID,
        XPAmount = challenge.XPReward,
        SourceType = "Challenge",
        SourceReferenceID = challenge.ChallengeID,
        CreatedAt = DateTime.Now
    });

    user.TotalXP += challenge.XPReward;

    if (user.TotalXP >= 350)
        user.CurrentLevel = 3;
    else if (user.TotalXP >= 150)
        user.CurrentLevel = 2;
    else
        user.CurrentLevel = 1;

    _context.SaveChanges();
    _achievementService.CheckAchievements(user.UserID);

    TempData["Success"] = $"Challenge completed! +{challenge.XPReward} XP";

    if (challengeId == 2)
{
    return RedirectToAction("Index", "Challenges");
}

return RedirectToAction("Index", "Challenges");
}
    }
}