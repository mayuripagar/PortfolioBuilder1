using System.ComponentModel.DataAnnotations;

namespace PortfolioBuilder1.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string ProjectName { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }
        public string? Technology { get; set; }
        public string? Link { get; set; }
        // Foreign key( User)
        public int UserId { get; set; }

        // Navigation property (must be the User type, not string)
        public User? User { get; set; }
    }
}