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
    public class MotorcycleModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotorcycleModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MotorcycleModels
        public async Task<IActionResult> Index()
        {
              return _context.MotorcycleModels != null ? 
                          View(await _context.MotorcycleModels.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MotorcycleModels'  is null.");
        }

        // GET: Admin/MotorcycleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MotorcycleModels == null)
            {
                return NotFound();
            }

            var motorcycleModel = await _context.MotorcycleModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycleModel == null)
            {
                return NotFound();
            }

            return View(motorcycleModel);
        }

        // GET: Admin/MotorcycleModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MotorcycleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModelName")] MotorcycleModel motorcycleModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motorcycleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motorcycleModel);
        }

        // GET: Admin/MotorcycleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MotorcycleModels == null)
            {
                return NotFound();
            }

            var motorcycleModel = await _context.MotorcycleModels.FindAsync(id);
            if (motorcycleModel == null)
            {
                return NotFound();
            }
            return View(motorcycleModel);
        }

        // POST: Admin/MotorcycleModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModelName")] MotorcycleModel motorcycleModel)
        {
            if (id != motorcycleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorcycleModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorcycleModelExists(motorcycleModel.Id))
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
            return View(motorcycleModel);
        }

        // GET: Admin/MotorcycleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MotorcycleModels == null)
            {
                return NotFound();
            }

            var motorcycleModel = await _context.MotorcycleModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycleModel == null)
            {
                return NotFound();
            }

            return View(motorcycleModel);
        }

        // POST: Admin/MotorcycleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MotorcycleModels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MotorcycleModels'  is null.");
            }
            var motorcycleModel = await _context.MotorcycleModels.FindAsync(id);
            if (motorcycleModel != null)
            {
                _context.MotorcycleModels.Remove(motorcycleModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotorcycleModelExists(int id)
        {
          return (_context.MotorcycleModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
