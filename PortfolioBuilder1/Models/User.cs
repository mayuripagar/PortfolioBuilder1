using PortfolioBuilder1.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortfolioBuilder1.Models;
public class User
{
    public int Id { get; set; }

    [Required]
    public string ? FullName { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string ?Password { get; set; }
    public Profile? Profile { get; set; }
 


    // Navigation Properties
    public ICollection<Skills> ?Skills { get; set; }
    public ICollection<Project>? Projects { get; set; }
    public ICollection<Education>? Educations { get; set; }
    public ICollection<Experience>? Experiences { get; set; }

   
}