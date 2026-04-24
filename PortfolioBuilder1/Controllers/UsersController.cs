using Microsoft.AspNetCore.Mvc;
using PortfolioBuilder1.Data;
using PortfolioBuilder1.Models;
using Microsoft.EntityFrameworkCore;

namespace PortfolioBuilder1.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // ======================
        // 1️⃣ LIST USERS
        // ======================
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // ======================
        // 2️⃣ CREATE (GET)
        // ======================
        public IActionResult Create()
        {
            return View();
        }

        // ======================
        // 3️⃣ CREATE (POST)
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Users.Add(user);
            //    _context.SaveChanges();
            //    ModelState.Clear();
            //    return RedirectToAction(nameof(Index));
            //}
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Content("Errors: " + string.Join(" | ", errors));
            }

            _context.Users.Add(user);
            _context.SaveChanges();

               ModelState.Clear();
            return RedirectToAction(nameof(Index));
        }

        // ======================
        // 4️⃣ EDIT (GET)
        // ======================
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // ======================
        // 5️⃣ EDIT (POST)
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                // Return diagnostics so you can see validation errors in the browser during debugging
                var errors = ModelState
                    .Where(kv => kv.Value.Errors.Count > 0)
                    .Select(kv => kv.Key + ": " + string.Join(", ", kv.Value.Errors.Select(e => e.ErrorMessage)))
                    .ToList();

                return Content("ModelState invalid: " + string.Join(" | ", errors));
            }

            try
            {
                _context.Update(user);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.Id == user.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

       
        //public IActionResult Edit(int id, User user)
        //{
        //    if (id != user.Id)
        //        return NotFound();

        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(user);
        //        _context.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(user);
        //}

        // ======================
        // 6️⃣ DELETE (GET)
        // ======================
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // ======================
        // 7️⃣ DELETE (POST)
        // ======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        // ======================
        // 8️⃣ DETAILS
        // ======================
        public IActionResult Details(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }
    }
}