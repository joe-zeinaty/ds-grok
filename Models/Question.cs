namespace D_S_Grok.Models
{
    public class Question
    {
        public int QuestionID { get; set; }

        public int QuizID { get; set; }
        public Quiz? Quiz { get; set; }

        public string QuestionText { get; set; } = string.Empty;

        public string Explanation { get; set; } = string.Empty;
    }
}