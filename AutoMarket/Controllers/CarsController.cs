using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoMarket.Data;
using AutoMarket.Models;
using System.Runtime.ConstrainedExecution;
using X.PagedList;
using X.PagedList.Mvc;

namespace AutoMarket.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 4; // Set your desired page size here

            var cars = await _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarColor)
                .Include(c => c.CarCondition)
                .Include(c => c.CarFuelType)
                .Include(c => c.CarMileage)
                .Include(c => c.CarModel)
                .Include(c => c.CarSeats)
                .Include(c => c.CarTransmissionType)
                .Include(c => c.CarVersion)
                .ToPagedListAsync(pageNumber, pageSize);

            return View(cars);

        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarColor)
                .Include(c => c.CarCondition)
                .Include(c => c.CarFuelType)
                .Include(c => c.CarMileage)
                .Include(c => c.CarModel)
                .Include(c => c.CarSeats)
                .Include(c => c.CarTransmissionType)
                .Include(c => c.CarVersion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["CarBrandId"] = new SelectList(_context.Brands, "Id", "BrandName");
            ViewData["CarColorId"] = new SelectList(_context.Colors, "Id", "Color");
            ViewData["CarConditionId"] = new SelectList(_context.Condition, "Id", "Condition");
            ViewData["CarFuelTypeId"] = new SelectList(_context.FuelTypes, "Id", "FuelType");
            ViewData["CarMileageId"] = new SelectList(_context.Mileages, "Id", "Mileage");
            ViewData["CarModelId"] = new SelectList(_context.Models, "Id", "ModelName");
            ViewData["CarSeatsId"] = new SelectList(_context.Seats, "Id", "NumberofSeats");
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.TransmissionTypes, "Id", "TransmissionType");
            ViewData["CarVersionId"] = new SelectList(_context.Versions, "Id", "VersionType");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstRegistration,EnginePower,Price,Features,Description,Location,CarBrandId,CarModelId,CarFuelTypeId,CarColorId,CarConditionId,CarMileageId,CarSeatsId,CarTransmissionTypeId,CarVersionId")] Car car)
        {
            
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarBrandId"] = new SelectList(_context.Brands, "Id", "BrandName", car.CarBrandId);
            ViewData["CarColorId"] = new SelectList(_context.Colors, "Id", "Color", car.CarColorId);
            ViewData["CarConditionId"] = new SelectList(_context.Condition, "Id", "Condition", car.CarConditionId);
            ViewData["CarFuelTypeId"] = new SelectList(_context.FuelTypes, "Id", "FuelType", car.CarFuelTypeId);
            ViewData["CarMileageId"] = new SelectList(_context.Mileages, "Id", "Mileage", car.CarMileageId);
            ViewData["CarModelId"] = new SelectList(_context.Models, "Id", "ModelName", car.CarModelId);
            ViewData["CarSeatsId"] = new SelectList(_context.Seats, "Id", "NumberofSeats", car.CarSeatsId);
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.TransmissionTypes, "Id", "TransmissionType", car.CarTransmissionTypeId);
            ViewData["CarVersionId"] = new SelectList(_context.Versions, "Id", "VersionType", car.CarVersionId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstRegistration,EnginePower,Price,Features,Description,Location,CarBrandId,CarModelId,CarFuelTypeId,CarColorId,CarConditionId,CarMileageId,CarSeatsId,CarTransmissionTypeId,CarVersionId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["CarBrandId"] = new SelectList(_context.Brands, "Id", "Id", car.CarBrandId);
            ViewData["CarColorId"] = new SelectList(_context.Colors, "Id", "Id", car.CarColorId);
            ViewData["CarConditionId"] = new SelectList(_context.Condition, "Id", "Id", car.CarConditionId);
            ViewData["CarFuelTypeId"] = new SelectList(_context.FuelTypes, "Id", "Id", car.CarFuelTypeId);
            ViewData["CarMileageId"] = new SelectList(_context.Mileages, "Id", "Id", car.CarMileageId);
            ViewData["CarModelId"] = new SelectList(_context.Models, "Id", "Id", car.CarModelId);
            ViewData["CarSeatsId"] = new SelectList(_context.Seats, "Id", "Id", car.CarSeatsId);
            ViewData["CarTransmissionTypeId"] = new SelectList(_context.TransmissionTypes, "Id", "Id", car.CarTransmissionTypeId);
            ViewData["CarVersionId"] = new SelectList(_context.Versions, "Id", "Id", car.CarVersionId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarColor)
                .Include(c => c.CarCondition)
                .Include(c => c.CarFuelType)
                .Include(c => c.CarMileage)
                .Include(c => c.CarModel)
                .Include(c => c.CarSeats)
                .Include(c => c.CarTransmissionType)
                .Include(c => c.CarVersion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
            }
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
          return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
