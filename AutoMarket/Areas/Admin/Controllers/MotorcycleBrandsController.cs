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
    public class MotorcycleBrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotorcycleBrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MotorcycleBrands
        public async Task<IActionResult> Index()
        {
              return _context.MotorcycleBrands != null ? 
                          View(await _context.MotorcycleBrands.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MotorcycleBrands'  is null.");
        }

        // GET: Admin/MotorcycleBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MotorcycleBrands == null)
            {
                return NotFound();
            }

            var motorcycleBrand = await _context.MotorcycleBrands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycleBrand == null)
            {
                return NotFound();
            }

            return View(motorcycleBrand);
        }

        // GET: Admin/MotorcycleBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MotorcycleBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrandName")] MotorcycleBrand motorcycleBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motorcycleBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motorcycleBrand);
        }

        // GET: Admin/MotorcycleBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MotorcycleBrands == null)
            {
                return NotFound();
            }

            var motorcycleBrand = await _context.MotorcycleBrands.FindAsync(id);
            if (motorcycleBrand == null)
            {
                return NotFound();
            }
            return View(motorcycleBrand);
        }

        // POST: Admin/MotorcycleBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrandName")] MotorcycleBrand motorcycleBrand)
        {
            if (id != motorcycleBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorcycleBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorcycleBrandExists(motorcycleBrand.Id))
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
            return View(motorcycleBrand);
        }

        // GET: Admin/MotorcycleBrands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MotorcycleBrands == null)
            {
                return NotFound();
            }

            var motorcycleBrand = await _context.MotorcycleBrands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycleBrand == null)
            {
                return NotFound();
            }

            return View(motorcycleBrand);
        }

        // POST: Admin/MotorcycleBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MotorcycleBrands == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MotorcycleBrands'  is null.");
            }
            var motorcycleBrand = await _context.MotorcycleBrands.FindAsync(id);
            if (motorcycleBrand != null)
            {
                _context.MotorcycleBrands.Remove(motorcycleBrand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotorcycleBrandExists(int id)
        {
          return (_context.MotorcycleBrands?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
