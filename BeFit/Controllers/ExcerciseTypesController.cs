using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Controllers
{
    public class ExcerciseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExcerciseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExcerciseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExcerciseType.ToListAsync());
        }

        // GET: ExcerciseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excerciseType = await _context.ExcerciseType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (excerciseType == null)
            {
                return NotFound();
            }

            return View(excerciseType);
        }
        [Authorize(Roles = "Administrator")]
        // GET: ExcerciseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExcerciseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ExcerciseType excerciseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(excerciseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(excerciseType);
        }

        // GET: ExcerciseTypes/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excerciseType = await _context.ExcerciseType.FindAsync(id);
            if (excerciseType == null)
            {
                return NotFound();
            }
            return View(excerciseType);
        }

        // POST: ExcerciseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ExcerciseType excerciseType)
        {
            if (id != excerciseType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(excerciseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExcerciseTypeExists(excerciseType.Id))
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
            return View(excerciseType);
        }

        // GET: ExcerciseTypes/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excerciseType = await _context.ExcerciseType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (excerciseType == null)
            {
                return NotFound();
            }

            return View(excerciseType);
        }

        // POST: ExcerciseTypes/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var excerciseType = await _context.ExcerciseType.FindAsync(id);
            if (excerciseType != null)
            {
                _context.ExcerciseType.Remove(excerciseType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExcerciseTypeExists(int id)
        {
            return _context.ExcerciseType.Any(e => e.Id == id);
        }
    }
}
