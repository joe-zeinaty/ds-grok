namespace D_S_Grok.Models
{
    public class QuizAttemptAnswer
    {
        public int QuizAttemptAnswerID { get; set; }

        public int QuizAttemptID { get; set; }
        public QuizAttempt? QuizAttempt { get; set; }

        public int QuestionID { get; set; }
        public Question? Question { get; set; }

        public int SelectedOptionID { get; set; }
        public QuestionOption? SelectedOption { get; set; }

        public bool IsCorrect { get; set; }
    }
}