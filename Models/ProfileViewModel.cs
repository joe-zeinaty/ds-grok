using System.ComponentModel.DataAnnotations;

namespace D_S_Grok.Models
{
    public class ProfileViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = "";

        public int TotalXP { get; set; }
        public int CurrentLevel { get; set; }
        public int AchievementCount { get; set; }

        public string? CurrentPassword { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public string? NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmNewPassword { get; set; }
    }
}