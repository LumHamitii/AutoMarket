using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoMarket.Data;
using AutoMarket.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutoMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CarColorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CarColors
        public async Task<IActionResult> Index()
        {
              return _context.Colors != null ? 
                          View(await _context.Colors.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Colors'  is null.");
        }

        // GET: Admin/CarColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var carColor = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carColor == null)
            {
                return NotFound();
            }

            return View(carColor);
        }

        // GET: Admin/CarColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CarColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Color")] CarColor carColor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carColor);
        }

        // GET: Admin/CarColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var carColor = await _context.Colors.FindAsync(id);
            if (carColor == null)
            {
                return NotFound();
            }
            return View(carColor);
        }

        // POST: Admin/CarColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Color")] CarColor carColor)
        {
            if (id != carColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarColorExists(carColor.Id))
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
            return View(carColor);
        }

        // GET: Admin/CarColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var carColor = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carColor == null)
            {
                return NotFound();
            }

            return View(carColor);
        }

        // POST: Admin/CarColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Colors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Colors'  is null.");
            }
            var carColor = await _context.Colors.FindAsync(id);
            if (carColor != null)
            {
                _context.Colors.Remove(carColor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarColorExists(int id)
        {
          return (_context.Colors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
