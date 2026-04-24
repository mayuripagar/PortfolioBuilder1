using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioBuilder1.Data;
using PortfolioBuilder1.Models;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class SkillsController : Controller
{
    private readonly AppDbContext _context;

    public SkillsController(AppDbContext context)
    {
        _context = context;
    }

    // LIST
    public IActionResult Index()
    {
        var skills = _context.Skills.Include(s => s.User).ToList();
        //var skills = _context.Skills.ToList();
        return View(skills);
    }

    // CREATE GET
    public IActionResult Create()
    {
        // return an empty model so the Create view has a non-null Model
        ViewBag.Users = new SelectList(_context.Users.ToList(), "Id", "FullName");
        return View(new Skills());
    }

    // CREATE POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Skills skill)
    {
        //if (ModelState.IsValid)
        //{
        //    _context.Skills.Add(skill);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        if (!_context.Users.Any(u => u.Id == skill.UserId))
        {
            ModelState.AddModelError(nameof(skill.UserId), "selected user does not exist");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Users = new SelectList(_context.Users.OrderBy(u => u.FullName).ToList(), "Id", "FullName", skill.UserId);
            return View(skill);
            //_context.Skills.Add(skill);
            //_context.SaveChanges();
            //return RedirectToAction("Index");
        }

        try
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Unable to save skill. Please try again or contact support.");
            ViewBag.Users = new SelectList(_context.Users.OrderBy(u => u.FullName).ToList(), "Id", "FullName", skill.UserId);
            return View(skill);
        }

        //ViewBag.Users = new SelectList(_context.Users.ToList(), "Id", "FullName", skill.UserId);
        //return View(skill);
    }

    // EDIT GET
    public IActionResult Edit(int id)
    {
        var skill = _context.Skills.Find(id);
        if (skill == null) return NotFound();
        ViewBag.Users = new SelectList(_context.Users.ToList(), "Id", "FullName", skill.UserId);
        return View(skill);
    }

    // EDIT POST
    [HttpPost] 
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Skills skill)
    {
        //if (ModelState.IsValid)
        //{
        //    _context.Skills.Update(skill);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        if (!_context.Users.Any(u => u.Id == skill.UserId))
        {
            ModelState.AddModelError(nameof(skill.UserId), "Selected user does not exist.");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Users = new SelectList(_context.Users.OrderBy(u => u.FullName).ToList(), "Id", "FullName", skill.UserId);
            return View(skill);
            //_context.Skills.Update(skill);
            //_context.SaveChanges();
            //return RedirectToAction("Index");
        }

        //ViewBag.Users = new SelectList(_context.Users.ToList(), "Id", "FullName", skill.UserId);
        //return View(skill);
        try
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Unable to update skill. Please try again.");
            ViewBag.Users = new SelectList(_context.Users.OrderBy(u => u.FullName).ToList(), "Id", "FullName", skill.UserId);
            return View(skill);
        }
    }

    // DELETE
    public IActionResult Delete(int id)
    {
        var skill = _context.Skills.Find(id);

        if (skill == null)
            return NotFound();

        return View(skill);
    }

    // ======================
    // 7️⃣ DELETE (POST)
    // ======================
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        {
            var skill = _context.Skills.Find(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
    public IActionResult Details(int id)
    {
        var skill = _context.Skills.Find(id);

        if (skill == null)
            return NotFound();

        return View(skill);
    }
}