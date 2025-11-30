using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using System.Security.Claims;

namespace BeFit.Controllers
{
    public class ExcercisesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
        public ExcercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Excercises
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Excercise.Include(e => e.ExcerciseType).Include(e => e.Session);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Excercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excercise = await _context.Excercise
                .Include(e => e.ExcerciseType)
                .Include(e => e.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (excercise == null)
            {
                return NotFound();
            }

            return View(excercise);
        }

        // GET: Excercises/Create
        public IActionResult Create()
        {
            ViewData["ExcerciseTypeId"] = new SelectList(_context.Set<ExcerciseType>(), "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.Set<Session>(), "Id", "Start");
            return View();
        }

        // POST: Excercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SessionId,ExcerciseTypeId,Weight,SeriesCount,RepsCount")] Excercise excercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(excercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExcerciseType"] = new SelectList(_context.ExcerciseType, "Id", "Name", excercise.ExcerciseTypeId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Start", excercise.SessionId);
            return View(excercise);
        }

        // GET: Excercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excercise = await _context.Excercise.FindAsync(id);
            if (excercise == null)
            {
                return NotFound();
            }
            ViewData["ExcerciseTypeId"] = new SelectList(_context.Set<ExcerciseType>(), "Id", "Id", excercise.ExcerciseTypeId);
            ViewData["SessionId"] = new SelectList(_context.Set<Session>(), "Id", "Id", excercise.SessionId);
            return View(excercise);
        }

        // POST: Excercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SessionId,ExcerciseTypeId,Weight,SeriesCount,RepsCount")] Excercise excercise)
        {
            if (id != excercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(excercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExcerciseExists(excercise.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExcerciseTypeId"] = new SelectList(_context.Set<ExcerciseType>(), "Id", "Id", excercise.ExcerciseTypeId);
            ViewData["SessionId"] = new SelectList(_context.Set<Session>(), "Id", "Id", excercise.SessionId);
            return View(excercise);
        }

        // GET: Excercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excercise = await _context.Excercise
                .Include(e => e.ExcerciseType)
                .Include(e => e.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (excercise == null)
            {
                return NotFound();
            }

            return View(excercise);
        }

        // POST: Excercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var excercise = await _context.Excercise.FindAsync(id);
            if (excercise != null)
            {
                _context.Excercise.Remove(excercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExcerciseExists(int id)
        {
            return _context.Excercise.Any(e => e.Id == id);
        }
    }
}
