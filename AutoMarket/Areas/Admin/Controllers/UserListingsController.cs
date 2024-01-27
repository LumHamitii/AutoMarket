using AutoMarket.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMarket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMarket.Data.Migrations;

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
        public async Task<IActionResult> UserDashboard()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userId = currentUser.Id;

            var userCars = _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarModel)
                .Where(c => c.User.Id == userId)
                .ToList();

            var userMotorcycles = _context.Motorcycles
                .Include(m => m.MotorcycleBrand)
                .Include(m => m.MotorcycleModel)
                .Where(m => m.User.Id == userId)
                .ToList();

            var viewModel = new UserDashboardViewModel
            {
                UserCars = userCars,
                UserMotorcycles = userMotorcycles
            };

            return View(viewModel);
        }

        public async Task<IActionResult> MyCars()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var userId = currentUser.Id;
            var userCars = _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarModel)
                .Where(c => c.User.Id == userId)
                .ToList();

            return View(userCars);
        }

        //GET Motorcycles
        public async Task<IActionResult> MyMotorcycles()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var userId = currentUser.Id;
            var userMotorcycles = _context.Motorcycles
                .Include(m => m.MotorcycleBrand)
                .Include(m => m.MotorcycleModel)
                .Where(m => m.User.Id == userId)
                .ToList();

            return View(userMotorcycles);
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
        // GET: Admin/UserListings/Edit/5
        public async Task<IActionResult> EditMotorcycle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _context.Motorcycles
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorcycle == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (motorcycle.User.Id != currentUser.Id)
            {
                return Forbid();
            }

            ViewData["MotorcycleBrandId"] = new SelectList(_context.MotorcycleBrands, "Id", "BrandName", motorcycle.MotorcycleBrandId);
            ViewData["MotorcycleColorId"] = new SelectList(_context.MotorcycleColors, "Id", "Color", motorcycle.MotorcycleColorId);
            ViewData["MotorcycleConditionId"] = new SelectList(_context.MotorcycleConditions, "Id", "Condition", motorcycle.MotorcycleConditionId);
            ViewData["MotorcycleFuelTypeId"] = new SelectList(_context.MotorcycleFuelTypes, "Id", "Fuel", motorcycle.MotorcycleFuelTypeId);
            ViewData["MotorcycleMileageId"] = new SelectList(_context.MotorcycleMileages, "Id", "Mileage", motorcycle.MotorcycleMileageId);
            ViewData["MotorcycleModelId"] = new SelectList(_context.MotorcycleModels, "Id", "ModelName", motorcycle.MotorcycleModelId);
            ViewData["MotorcycleTransmissionId"] = new SelectList(_context.MotorcycleTransmissions, "Id", "Transmission", motorcycle.MotorcycleTransmissionId);
            ViewData["MotorcycleTypeId"] = new SelectList(_context.MotorcycleTypes, "Id", "Type", motorcycle.MotorcycleTypeId);

            return View(motorcycle);
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
            return RedirectToAction(nameof(UserDashboard));
        }
        // POST: Admin/UserListings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMotorcycle(int id, [Bind("Id,FirstRegistration,EnginePower,Price,Description,Location,MotorcycleBrandId,MotorcycleModelId,MotorcycleTypeId,MotorcycleColorId,MotorcycleMileageId,MotorcycleConditionId,MotorcycleTransmissionId,MotorcycleFuelTypeId")] Motorcycle motorcycle)
        {
            if (id != motorcycle.Id)
            {
                return NotFound();
            }

            var existingMotorcycle = await _context.Motorcycles
                .Include(m => m.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            var currentUser = await _userManager.GetUserAsync(User);
            if (existingMotorcycle.User.Id != currentUser.Id)
            {
                return Forbid();
            }

            try
            {
                _context.Entry(existingMotorcycle).CurrentValues.SetValues(motorcycle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Motorcycles.AnyAsync(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(UserDashboard));
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
        // GET: Admin/UserListings/Delete/5 for motorcycle
        public async Task<IActionResult> DeleteMotorcycle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _context.Motorcycles
                .Include(m => m.MotorcycleBrand)
                .Include(m => m.MotorcycleColor)
                .Include(m => m.MotorcycleCondition)
                .Include(m => m.MotorcycleFuelType)
                .Include(m => m.MotorcycleMileage)
                .Include(m => m.MotorcycleModel)
                .Include(m => m.MotorcycleTransmission)
                .Include(m => m.MotorcycleType)
                .Include(m => m.MotorcyclePhotos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorcycle == null)
            {
                return NotFound();
            }

            // Check if the user is the owner of the listing
            var currentUser = await _userManager.GetUserAsync(User);
            if (motorcycle.User.Id != currentUser.Id)
            {
                return Forbid(); // Or return a different view/page for unauthorized access
            }

            return View(motorcycle);
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
            return RedirectToAction(nameof(UserDashboard));
        }

        // POST: Admin/UserListings/Delete/5 for motorcycle
        [HttpPost, ActionName("DeleteMotorcycle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMotorcycleConfirmed(int id)
        {
            var motorcycle = await _context.Motorcycles.FindAsync(id);

            // Check if the user is the owner of the listing
            var currentUser = await _userManager.GetUserAsync(User);
            if (motorcycle.User.Id != currentUser.Id)
            {
                return Forbid();
            }

            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserDashboard));
        }

    }
}
