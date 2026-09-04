using System.ComponentModel.DataAnnotations;

namespace D_S_Grok.Models
{
    public class Lesson
    {
        public int LessonID { get; set; }

        public int LevelID { get; set; }
        public Level? Level { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int LessonOrder { get; set; }

        public int XPReward { get; set; }
    }
}