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
    public class TruckConditionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TruckConditionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TruckConditions
        public async Task<IActionResult> Index()
        {
              return _context.TruckConditions != null ? 
                          View(await _context.TruckConditions.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TruckConditions'  is null.");
        }

        // GET: Admin/TruckConditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TruckConditions == null)
            {
                return NotFound();
            }

            var truckCondition = await _context.TruckConditions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckCondition == null)
            {
                return NotFound();
            }

            return View(truckCondition);
        }

        // GET: Admin/TruckConditions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TruckConditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Condition")] TruckCondition truckCondition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truckCondition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(truckCondition);
        }

        // GET: Admin/TruckConditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TruckConditions == null)
            {
                return NotFound();
            }

            var truckCondition = await _context.TruckConditions.FindAsync(id);
            if (truckCondition == null)
            {
                return NotFound();
            }
            return View(truckCondition);
        }

        // POST: Admin/TruckConditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Condition")] TruckCondition truckCondition)
        {
            if (id != truckCondition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckCondition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckConditionExists(truckCondition.Id))
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
            return View(truckCondition);
        }

        // GET: Admin/TruckConditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TruckConditions == null)
            {
                return NotFound();
            }

            var truckCondition = await _context.TruckConditions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckCondition == null)
            {
                return NotFound();
            }

            return View(truckCondition);
        }

        // POST: Admin/TruckConditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TruckConditions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TruckConditions'  is null.");
            }
            var truckCondition = await _context.TruckConditions.FindAsync(id);
            if (truckCondition != null)
            {
                _context.TruckConditions.Remove(truckCondition);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckConditionExists(int id)
        {
          return (_context.TruckConditions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
