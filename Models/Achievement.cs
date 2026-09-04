using System.ComponentModel.DataAnnotations;

namespace D_S_Grok.Models
{
    public class Achievement
    {
        public int AchievementID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string RequirementType { get; set; } = string.Empty;

        public int RequirementValue { get; set; }

        public int XPReward { get; set; }
    }
}