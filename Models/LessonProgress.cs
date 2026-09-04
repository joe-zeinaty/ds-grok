namespace D_S_Grok.Models
{
    public class LessonProgress
    {
        public int LessonProgressID { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        public int LessonID { get; set; }
        public Lesson? Lesson { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime? CompletedAt { get; set; }
    }
}