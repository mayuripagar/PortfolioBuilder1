using Microsoft.AspNetCore.Mvc;
using PortfolioBuilder1.Data;
using PortfolioBuilder1.Models;
using PortfolioBuilder1.ViewModels;

public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = new DashboardViewModel
        {
            TotalSkills = _context.Skills.Count(),
            TotalProjects = _context.Projects.Count(),
            TotalEducation = _context.Educations.Count()
        };

        return View(model);
    }
}