using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using D_S_Grok.Data;
using D_S_Grok.Models;
using D_S_Grok.Services;

namespace D_S_Grok.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AchievementService _achievementService;

        public QuizzesController(
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

            var quizzes = _context.Quizzes
                .Include(q => q.Level)
                .OrderBy(q => q.LevelID)
                .ToList();

            return View(quizzes);
        }

        public IActionResult Take(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var quiz = _context.Quizzes
                .Include(q => q.Level)
                .FirstOrDefault(q => q.QuizID == id);

            if (quiz == null)
            {
                return RedirectToAction("Index");
            }

            var questions = _context.Questions
                .Where(q => q.QuizID == id)
                .Include(q => q.Quiz)
                .ToList();

            ViewBag.Quiz = quiz;
            ViewBag.Options = _context.QuestionOptions.ToList();

            return View(questions);
        }

        [HttpPost]
        public IActionResult Submit(int quizId, Dictionary<int, int> answers)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var quiz = _context.Quizzes.FirstOrDefault(q => q.QuizID == quizId);

            if (quiz == null)
            {
                return RedirectToAction("Index");
            }

            var questions = _context.Questions
                .Where(q => q.QuizID == quizId)
                .ToList();

            int correctCount = 0;

            var attempt = new QuizAttempt
            {
                UserID = userId.Value,
                QuizID = quizId,
                StartedAt = DateTime.Now,
                CompletedAt = DateTime.Now
            };

            _context.QuizAttempts.Add(attempt);
            _context.SaveChanges();

            foreach (var question in questions)
            {
                if (answers.ContainsKey(question.QuestionID))
                {
                    int selectedOptionId = answers[question.QuestionID];

                    var selectedOption = _context.QuestionOptions
                        .FirstOrDefault(o => o.OptionID == selectedOptionId);

                    bool isCorrect = selectedOption != null && selectedOption.IsCorrect;

                    if (isCorrect)
                    {
                        correctCount++;
                    }

                    _context.QuizAttemptAnswers.Add(new QuizAttemptAnswer
                    {
                        QuizAttemptID = attempt.QuizAttemptID,
                        QuestionID = question.QuestionID,
                        SelectedOptionID = selectedOptionId,
                        IsCorrect = isCorrect
                    });
                }
            }

            int scorePercentage = questions.Count == 0
                ? 0
                : (correctCount * 100) / questions.Count;

            attempt.ScorePercentage = scorePercentage;
            attempt.IsPassed = scorePercentage >= quiz.PassMark;

            if (attempt.IsPassed)
            {
                var user = _context.Users.First(u => u.UserID == userId.Value);

                user.TotalXP += quiz.XPReward;

                if (user.TotalXP >= 350)
                    user.CurrentLevel = 3;
                else if (user.TotalXP >= 150)
                    user.CurrentLevel = 2;
                else
                    user.CurrentLevel = 1;

                _context.XPTransactions.Add(new XPTransaction
                {
                    UserID = user.UserID,
                    XPAmount = quiz.XPReward,
                    SourceType = "Quiz",
                    SourceReferenceID = quiz.QuizID,
                    CreatedAt = DateTime.Now
                });
            }

            _context.SaveChanges();

            _achievementService.CheckAchievements(userId.Value);

            TempData["QuizScore"] = scorePercentage;
            TempData["QuizPassed"] = attempt.IsPassed ? "true" : "false";

            return RedirectToAction("Result");
        }

        public IActionResult Result()
        {
            return View();
        }
    }
}