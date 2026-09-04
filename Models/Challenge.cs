using System.ComponentModel.DataAnnotations;

namespace D_S_Grok.Models
{
    public class Challenge
    {
        public int ChallengeID { get; set; }

        public int LevelID { get; set; }
        public Level? Level { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ChallengeType { get; set; } = string.Empty;

        public int XPReward { get; set; }
    }
}