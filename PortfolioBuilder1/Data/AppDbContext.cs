using Microsoft.EntityFrameworkCore;
using PortfolioBuilder1.Models;
namespace PortfolioBuilder1.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Skills> Skills { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Profile> Profiles { get; set; }
}