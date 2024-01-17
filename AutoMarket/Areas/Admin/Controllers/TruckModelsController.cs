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
    public class TruckModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TruckModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TruckModels
        public async Task<IActionResult> Index()
        {
              return _context.TruckModels != null ? 
                          View(await _context.TruckModels.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TruckModels'  is null.");
        }

        // GET: Admin/TruckModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TruckModels == null)
            {
                return NotFound();
            }

            var truckModel = await _context.TruckModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckModel == null)
            {
                return NotFound();
            }

            return View(truckModel);
        }

        // GET: Admin/TruckModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TruckModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModelName")] TruckModel truckModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truckModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(truckModel);
        }

        // GET: Admin/TruckModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TruckModels == null)
            {
                return NotFound();
            }

            var truckModel = await _context.TruckModels.FindAsync(id);
            if (truckModel == null)
            {
                return NotFound();
            }
            return View(truckModel);
        }

        // POST: Admin/TruckModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModelName")] TruckModel truckModel)
        {
            if (id != truckModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckModelExists(truckModel.Id))
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
            return View(truckModel);
        }

        // GET: Admin/TruckModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TruckModels == null)
            {
                return NotFound();
            }

            var truckModel = await _context.TruckModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckModel == null)
            {
                return NotFound();
            }

            return View(truckModel);
        }

        // POST: Admin/TruckModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TruckModels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TruckModels'  is null.");
            }
            var truckModel = await _context.TruckModels.FindAsync(id);
            if (truckModel != null)
            {
                _context.TruckModels.Remove(truckModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckModelExists(int id)
        {
          return (_context.TruckModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
