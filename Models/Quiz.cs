using System.ComponentModel.DataAnnotations;

namespace D_S_Grok.Models
{
    public class Quiz
    {
        public int QuizID { get; set; }

        public int LevelID { get; set; }
        public Level? Level { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public int PassMark { get; set; }

        public int XPReward { get; set; }
    }
}