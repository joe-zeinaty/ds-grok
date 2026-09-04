namespace D_S_Grok.Models
{
    public class QuizAttempt
    {
        public int QuizAttemptID { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        public int QuizID { get; set; }
        public Quiz? Quiz { get; set; }

        public int ScorePercentage { get; set; }

        public bool IsPassed { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.Now;

        public DateTime? CompletedAt { get; set; }
    }
}