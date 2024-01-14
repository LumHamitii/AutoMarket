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
    public class MotorcycleConditionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotorcycleConditionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MotorcycleConditions
        public async Task<IActionResult> Index()
        {
              return _context.MotorcycleConditions != null ? 
                          View(await _context.MotorcycleConditions.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MotorcycleConditions'  is null.");
        }

        // GET: Admin/MotorcycleConditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MotorcycleConditions == null)
            {
                return NotFound();
            }

            var motorcycleCondition = await _context.MotorcycleConditions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycleCondition == null)
            {
                return NotFound();
            }

            return View(motorcycleCondition);
        }

        // GET: Admin/MotorcycleConditions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MotorcycleConditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Condition")] MotorcycleCondition motorcycleCondition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motorcycleCondition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motorcycleCondition);
        }

        // GET: Admin/MotorcycleConditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MotorcycleConditions == null)
            {
                return NotFound();
            }

            var motorcycleCondition = await _context.MotorcycleConditions.FindAsync(id);
            if (motorcycleCondition == null)
            {
                return NotFound();
            }
            return View(motorcycleCondition);
        }

        // POST: Admin/MotorcycleConditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Condition")] MotorcycleCondition motorcycleCondition)
        {
            if (id != motorcycleCondition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorcycleCondition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorcycleConditionExists(motorcycleCondition.Id))
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
            return View(motorcycleCondition);
        }

        // GET: Admin/MotorcycleConditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MotorcycleConditions == null)
            {
                return NotFound();
            }

            var motorcycleCondition = await _context.MotorcycleConditions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycleCondition == null)
            {
                return NotFound();
            }

            return View(motorcycleCondition);
        }

        // POST: Admin/MotorcycleConditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MotorcycleConditions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MotorcycleConditions'  is null.");
            }
            var motorcycleCondition = await _context.MotorcycleConditions.FindAsync(id);
            if (motorcycleCondition != null)
            {
                _context.MotorcycleConditions.Remove(motorcycleCondition);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotorcycleConditionExists(int id)
        {
          return (_context.MotorcycleConditions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
