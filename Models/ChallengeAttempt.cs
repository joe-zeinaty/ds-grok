namespace D_S_Grok.Models
{
    public class ChallengeAttempt
    {
        public int ChallengeAttemptID { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        public int ChallengeID { get; set; }
        public Challenge? Challenge { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime? CompletedAt { get; set; }

        public int AttemptsCount { get; set; } = 0;
    }
}