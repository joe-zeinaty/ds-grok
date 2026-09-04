using System.ComponentModel.DataAnnotations;

namespace D_S_Grok.Models
{
    public class XPTransaction
    {
        public int XPTransactionID { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        public int XPAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string SourceType { get; set; } = string.Empty;

        public int SourceReferenceID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}