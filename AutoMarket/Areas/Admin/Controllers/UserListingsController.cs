using AutoMarket.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMarket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "User")]
    public class UserListingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public UserListingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyCars()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var userId = currentUser.Id;
            var userCars = _context.Cars
                .Where(c => c.User.Id == userId)
                .ToList();

            return View(userCars);
        }
        // GET: Admin/UserListings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (car.User.Id != currentUser.Id)
            {
                return Forbid(); 
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

        // POST: Admin/UserListings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstRegistration,EnginePower,Price,Features,Description,Location,CarBrandId,CarModelId,CarFuelTypeId,CarColorId,CarConditionId,CarMileageId,CarSeatsId,CarTransmissionTypeId,CarVersionId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            var existingCar = await _context.Cars
                .Include(c => c.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            var currentUser = await _userManager.GetUserAsync(User);
            if (existingCar.User.Id != currentUser.Id)
            {
                return Forbid(); 
            }

            try
            {
                _context.Entry(existingCar).CurrentValues.SetValues(car);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Cars.AnyAsync(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(MyCars));
        }


        // GET: Admin/UserListings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
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
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            // Check if the user is the owner of the listing
            var currentUser = await _userManager.GetUserAsync(User);
            if (car.User.Id != currentUser.Id)
            {
                return Forbid(); // Or return a different view/page for unauthorized access
            }

            return View(car);
        }

        // POST: Admin/UserListings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            // Check if the user is the owner of the listing
            var currentUser = await _userManager.GetUserAsync(User);
            if (car.User.Id != currentUser.Id)
            {
                return Forbid(); 
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyCars));
        }

    }
}
