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
    public class TruckFuelTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TruckFuelTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TruckFuelTypes
        public async Task<IActionResult> Index()
        {
              return _context.TruckFuelTypes != null ? 
                          View(await _context.TruckFuelTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TruckFuelTypes'  is null.");
        }

        // GET: Admin/TruckFuelTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TruckFuelTypes == null)
            {
                return NotFound();
            }

            var truckFuelType = await _context.TruckFuelTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckFuelType == null)
            {
                return NotFound();
            }

            return View(truckFuelType);
        }

        // GET: Admin/TruckFuelTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TruckFuelTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FuelType")] TruckFuelType truckFuelType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truckFuelType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(truckFuelType);
        }

        // GET: Admin/TruckFuelTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TruckFuelTypes == null)
            {
                return NotFound();
            }

            var truckFuelType = await _context.TruckFuelTypes.FindAsync(id);
            if (truckFuelType == null)
            {
                return NotFound();
            }
            return View(truckFuelType);
        }

        // POST: Admin/TruckFuelTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FuelType")] TruckFuelType truckFuelType)
        {
            if (id != truckFuelType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckFuelType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckFuelTypeExists(truckFuelType.Id))
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
            return View(truckFuelType);
        }

        // GET: Admin/TruckFuelTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TruckFuelTypes == null)
            {
                return NotFound();
            }

            var truckFuelType = await _context.TruckFuelTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckFuelType == null)
            {
                return NotFound();
            }

            return View(truckFuelType);
        }

        // POST: Admin/TruckFuelTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TruckFuelTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TruckFuelTypes'  is null.");
            }
            var truckFuelType = await _context.TruckFuelTypes.FindAsync(id);
            if (truckFuelType != null)
            {
                _context.TruckFuelTypes.Remove(truckFuelType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckFuelTypeExists(int id)
        {
          return (_context.TruckFuelTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
