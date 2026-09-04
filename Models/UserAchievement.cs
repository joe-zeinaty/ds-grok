namespace D_S_Grok.Models
{
    public class UserAchievement
    {
        public int UserAchievementID { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        public int AchievementID { get; set; }
        public Achievement? Achievement { get; set; }

        public DateTime EarnedAt { get; set; } = DateTime.Now;
    }
}