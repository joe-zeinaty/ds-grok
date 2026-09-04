using System.ComponentModel.DataAnnotations;

namespace D_S_Grok.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public int TotalXP { get; set; } = 0;

        public int CurrentLevel { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}