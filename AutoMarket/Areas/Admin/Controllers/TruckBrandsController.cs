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
    public class TruckBrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TruckBrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TruckBrands
        public async Task<IActionResult> Index()
        {
              return _context.TruckBrands != null ? 
                          View(await _context.TruckBrands.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TruckBrands'  is null.");
        }

        // GET: Admin/TruckBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TruckBrands == null)
            {
                return NotFound();
            }

            var truckBrand = await _context.TruckBrands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckBrand == null)
            {
                return NotFound();
            }

            return View(truckBrand);
        }

        // GET: Admin/TruckBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TruckBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrandName")] TruckBrand truckBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truckBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(truckBrand);
        }

        // GET: Admin/TruckBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TruckBrands == null)
            {
                return NotFound();
            }

            var truckBrand = await _context.TruckBrands.FindAsync(id);
            if (truckBrand == null)
            {
                return NotFound();
            }
            return View(truckBrand);
        }

        // POST: Admin/TruckBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrandName")] TruckBrand truckBrand)
        {
            if (id != truckBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckBrandExists(truckBrand.Id))
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
            return View(truckBrand);
        }

        // GET: Admin/TruckBrands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TruckBrands == null)
            {
                return NotFound();
            }

            var truckBrand = await _context.TruckBrands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckBrand == null)
            {
                return NotFound();
            }

            return View(truckBrand);
        }

        // POST: Admin/TruckBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TruckBrands == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TruckBrands'  is null.");
            }
            var truckBrand = await _context.TruckBrands.FindAsync(id);
            if (truckBrand != null)
            {
                _context.TruckBrands.Remove(truckBrand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckBrandExists(int id)
        {
          return (_context.TruckBrands?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
