using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioBuilder1.Models
{
    public class Skills
    {
        public int Id { get; set; }

        [Required]
        public string ?  SkillName { get; set; }

        public string? SkillLevel { get; set; } // Beginner / Intermediate / Expert

        // Foreign Key
        public int UserId { get; set; }

        // Navigation
        public User ?User { get; set; }
    }
}