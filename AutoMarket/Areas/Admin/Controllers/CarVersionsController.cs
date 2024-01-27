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

    public class CarVersionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarVersionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CarVersions
        public async Task<IActionResult> Index()
        {
              return _context.Versions != null ? 
                          View(await _context.Versions.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Versions'  is null.");
        }

        // GET: Admin/CarVersions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Versions == null)
            {
                return NotFound();
            }

            var carVersion = await _context.Versions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carVersion == null)
            {
                return NotFound();
            }

            return View(carVersion);
        }

        // GET: Admin/CarVersions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CarVersions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VersionType")] CarVersion carVersion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carVersion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carVersion);
        }

        // GET: Admin/CarVersions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Versions == null)
            {
                return NotFound();
            }

            var carVersion = await _context.Versions.FindAsync(id);
            if (carVersion == null)
            {
                return NotFound();
            }
            return View(carVersion);
        }

        // POST: Admin/CarVersions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VersionType")] CarVersion carVersion)
        {
            if (id != carVersion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carVersion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarVersionExists(carVersion.Id))
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
            return View(carVersion);
        }

        // GET: Admin/CarVersions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Versions == null)
            {
                return NotFound();
            }

            var carVersion = await _context.Versions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carVersion == null)
            {
                return NotFound();
            }

            return View(carVersion);
        }

        // POST: Admin/CarVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Versions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Versions'  is null.");
            }
            var carVersion = await _context.Versions.FindAsync(id);
            if (carVersion != null)
            {
                _context.Versions.Remove(carVersion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarVersionExists(int id)
        {
          return (_context.Versions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
