using System.ComponentModel.DataAnnotations;

namespace D_S_Grok.Models
{
    public class Level
    {
        public int LevelID { get; set; }

        [Required]
        public int LevelNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int RequiredXP { get; set; }
    }
}