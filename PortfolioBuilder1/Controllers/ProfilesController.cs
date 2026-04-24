using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioBuilder1.Data;
using PortfolioBuilder1.Models;
using System.Threading.Tasks;
using System.Linq;

namespace PortfolioBuilder1.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly AppDbContext _context;

        public ProfilesController(AppDbContext context)
        {
            _context = context;
        }

        // READ (List)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profiles.ToListAsync());
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Profile profile)
        {
            // server-side validation: ensure the referenced User exists
            if (!_context.Users.Any(u => u.Id == profile.UserId))
            {
                ModelState.AddModelError(nameof(profile.UserId), "Selected user does not exist. Please choose an existing user.");
            }

            if (!ModelState.IsValid)
            {
                return View(profile);
            }

            try
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                // safe user-facing message on DB errors
                ModelState.AddModelError(string.Empty, "An error occurred while saving. Please try again.");
                return View(profile);
            }
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Profile profile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(profile);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(profile);
        //}

        // EDIT (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null) return NotFound();

            return View(profile);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Profile profile)
        {
            if (id != profile.Id) return NotFound();

            if (!_context.Users.Any(u => u.Id == profile.UserId))
            {
                ModelState.AddModelError(nameof(profile.UserId), "Selected user does not exist. Please choose an existing user.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Profiles.Any(e => e.Id == profile.Id)) return NotFound();
                    throw;
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving. Please try again.");
                }
            }

            return View(profile);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Profile profile)
        //{
        //    if (id != profile.Id) return NotFound();

        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(profile);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(profile);
        //}

        // DELETE (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (profile == null) return NotFound();

            return View(profile);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (profile == null) return NotFound();

            return View(profile);
        }
    }
}