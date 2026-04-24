using System.ComponentModel.DataAnnotations;

namespace PortfolioBuilder1.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string ?Title { get; set; }

        public string ?About { get; set; }  
        public string  Email { get; set; }

        public string Phone { get; set; }

        public string LinkedIn { get; set; }

        public string GitHub { get; set; }
        //Foreign key to User
        public int UserId { get; set; }

        public User? User { get; set; }
    }
}