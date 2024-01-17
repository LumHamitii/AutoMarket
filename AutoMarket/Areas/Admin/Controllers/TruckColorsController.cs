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
    public class TruckColorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TruckColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TruckColors
        public async Task<IActionResult> Index()
        {
              return _context.TruckColors != null ? 
                          View(await _context.TruckColors.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TruckColors'  is null.");
        }

        // GET: Admin/TruckColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TruckColors == null)
            {
                return NotFound();
            }

            var truckColor = await _context.TruckColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckColor == null)
            {
                return NotFound();
            }

            return View(truckColor);
        }

        // GET: Admin/TruckColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TruckColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Color")] TruckColor truckColor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truckColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(truckColor);
        }

        // GET: Admin/TruckColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TruckColors == null)
            {
                return NotFound();
            }

            var truckColor = await _context.TruckColors.FindAsync(id);
            if (truckColor == null)
            {
                return NotFound();
            }
            return View(truckColor);
        }

        // POST: Admin/TruckColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Color")] TruckColor truckColor)
        {
            if (id != truckColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckColorExists(truckColor.Id))
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
            return View(truckColor);
        }

        // GET: Admin/TruckColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TruckColors == null)
            {
                return NotFound();
            }

            var truckColor = await _context.TruckColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckColor == null)
            {
                return NotFound();
            }

            return View(truckColor);
        }

        // POST: Admin/TruckColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TruckColors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TruckColors'  is null.");
            }
            var truckColor = await _context.TruckColors.FindAsync(id);
            if (truckColor != null)
            {
                _context.TruckColors.Remove(truckColor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckColorExists(int id)
        {
          return (_context.TruckColors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
