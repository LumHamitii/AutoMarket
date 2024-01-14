using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoMarket.Data;
using AutoMarket.Models;

namespace AutoMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MotorcycleColorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotorcycleColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MotorcycleColors
        public async Task<IActionResult> Index()
        {
              return _context.MotorcycleColors != null ? 
                          View(await _context.MotorcycleColors.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MotorcycleColors'  is null.");
        }

        // GET: Admin/MotorcycleColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MotorcycleColors == null)
            {
                return NotFound();
            }

            var motorcycleColor = await _context.MotorcycleColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycleColor == null)
            {
                return NotFound();
            }

            return View(motorcycleColor);
        }

        // GET: Admin/MotorcycleColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MotorcycleColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Color")] MotorcycleColor motorcycleColor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motorcycleColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motorcycleColor);
        }

        // GET: Admin/MotorcycleColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MotorcycleColors == null)
            {
                return NotFound();
            }

            var motorcycleColor = await _context.MotorcycleColors.FindAsync(id);
            if (motorcycleColor == null)
            {
                return NotFound();
            }
            return View(motorcycleColor);
        }

        // POST: Admin/MotorcycleColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Color")] MotorcycleColor motorcycleColor)
        {
            if (id != motorcycleColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorcycleColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorcycleColorExists(motorcycleColor.Id))
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
            return View(motorcycleColor);
        }

        // GET: Admin/MotorcycleColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MotorcycleColors == null)
            {
                return NotFound();
            }

            var motorcycleColor = await _context.MotorcycleColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycleColor == null)
            {
                return NotFound();
            }

            return View(motorcycleColor);
        }

        // POST: Admin/MotorcycleColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MotorcycleColors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MotorcycleColors'  is null.");
            }
            var motorcycleColor = await _context.MotorcycleColors.FindAsync(id);
            if (motorcycleColor != null)
            {
                _context.MotorcycleColors.Remove(motorcycleColor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotorcycleColorExists(int id)
        {
          return (_context.MotorcycleColors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
