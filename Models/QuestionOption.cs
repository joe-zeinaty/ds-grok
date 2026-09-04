using System.ComponentModel.DataAnnotations;

namespace D_S_Grok.Models
{
    public class QuestionOption
    {
        public int OptionID { get; set; }

        public int QuestionID { get; set; }
        public Question? Question { get; set; }

        [Required]
        [StringLength(255)]
        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}