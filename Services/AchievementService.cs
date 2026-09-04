using D_S_Grok.Data;
using D_S_Grok.Models;

namespace D_S_Grok.Services
{
    public class AchievementService
    {
        private readonly ApplicationDbContext _context;

        public AchievementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CheckAchievements(int userId)
        {
            AwardFirstSteps(userId);
            AwardPointerExplorer(userId);
            AwardListMaster(userId);
            AwardLinkedListExpert(userId);

            _context.SaveChanges();
        }

        private void AwardFirstSteps(int userId)
        {
            int completedLessons = _context.LessonProgress
                .Count(lp => lp.UserID == userId && lp.IsCompleted);

            if (completedLessons >= 1)
            {
                AwardAchievement(userId, 1);
            }
        }

        private void AwardPointerExplorer(int userId)
        {
            bool completedLevel1Lessons = _context.LessonProgress
                .Where(lp => lp.UserID == userId && lp.IsCompleted)
                .Join(_context.Lessons,
                    lp => lp.LessonID,
                    lesson => lesson.LessonID,
                    (lp, lesson) => lesson)
                .Count(lesson => lesson.LevelID == 1) >= 2;

            if (completedLevel1Lessons)
            {
                AwardAchievement(userId, 2);
            }
        }

        private void AwardListMaster(int userId)
        {
            bool completedTraversalChallenge = _context.ChallengeAttempts
                .Any(ca =>
                    ca.UserID == userId &&
                    ca.ChallengeID == 1 &&
                    ca.IsCompleted);

            if (completedTraversalChallenge)
            {
                AwardAchievement(userId, 3);
            }
        }

        private void AwardLinkedListExpert(int userId)
        {
            bool completedTraversalChallenge = _context.ChallengeAttempts
                .Any(ca =>
                    ca.UserID == userId &&
                    ca.ChallengeID == 1 &&
                    ca.IsCompleted);

            bool completedOperationsChallenge = _context.ChallengeAttempts
                .Any(ca =>
                    ca.UserID == userId &&
                    ca.ChallengeID == 2 &&
                    ca.IsCompleted);

            if (completedTraversalChallenge && completedOperationsChallenge)
            {
                AwardAchievement(userId, 4);
            }
        }

        private void AwardAchievement(int userId, int achievementId)
        {
            bool alreadyAwarded = _context.UserAchievements
                .Any(ua =>
                    ua.UserID == userId &&
                    ua.AchievementID == achievementId);

            if (alreadyAwarded)
            {
                return;
            }

            var achievement = _context.Achievements
                .First(a => a.AchievementID == achievementId);

            _context.UserAchievements.Add(new UserAchievement
            {
                UserID = userId,
                AchievementID = achievement.AchievementID,
                EarnedAt = DateTime.Now
            });

            _context.XPTransactions.Add(new XPTransaction
            {
                UserID = userId,
                XPAmount = achievement.XPReward,
                SourceType = "Achievement",
                SourceReferenceID = achievement.AchievementID,
                CreatedAt = DateTime.Now
            });

            var user = _context.Users.First(u => u.UserID == userId);
            user.TotalXP += achievement.XPReward;
        }
    }
}