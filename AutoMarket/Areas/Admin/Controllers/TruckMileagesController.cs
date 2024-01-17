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
    public class TruckMileagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TruckMileagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TruckMileages
        public async Task<IActionResult> Index()
        {
              return _context.TruckMileages != null ? 
                          View(await _context.TruckMileages.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TruckMileages'  is null.");
        }

        // GET: Admin/TruckMileages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TruckMileages == null)
            {
                return NotFound();
            }

            var truckMileage = await _context.TruckMileages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckMileage == null)
            {
                return NotFound();
            }

            return View(truckMileage);
        }

        // GET: Admin/TruckMileages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TruckMileages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mileage")] TruckMileage truckMileage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truckMileage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(truckMileage);
        }

        // GET: Admin/TruckMileages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TruckMileages == null)
            {
                return NotFound();
            }

            var truckMileage = await _context.TruckMileages.FindAsync(id);
            if (truckMileage == null)
            {
                return NotFound();
            }
            return View(truckMileage);
        }

        // POST: Admin/TruckMileages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mileage")] TruckMileage truckMileage)
        {
            if (id != truckMileage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckMileage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckMileageExists(truckMileage.Id))
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
            return View(truckMileage);
        }

        // GET: Admin/TruckMileages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TruckMileages == null)
            {
                return NotFound();
            }

            var truckMileage = await _context.TruckMileages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckMileage == null)
            {
                return NotFound();
            }

            return View(truckMileage);
        }

        // POST: Admin/TruckMileages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TruckMileages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TruckMileages'  is null.");
            }
            var truckMileage = await _context.TruckMileages.FindAsync(id);
            if (truckMileage != null)
            {
                _context.TruckMileages.Remove(truckMileage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckMileageExists(int id)
        {
          return (_context.TruckMileages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
