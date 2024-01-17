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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMarket.Authorization;
using AutoMarket.ViewModel;
using System.Diagnostics;
namespace AutoMarket.Controllers
{
    public class TrucksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TrucksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Trucks
        public async Task<IActionResult> Index()
        {
            var trucks = await _context.Truck
                .Include(t => t.TruckBrand)
                .Include(t => t.TruckColor)
                .Include(t => t.TruckCondition)
                .Include(t => t.TruckFuelType)
                .Include(t => t.TruckMileage)
                .Include(t => t.TruckModel)
                .Include(t => t.TruckTransmissionType)
                .Include(t => t.TruckVersion)
                .Include(t => t.User)
                .ToListAsync();

            return View(trucks);
        }

        /// GET: Trucks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Truck
                .Include(t => t.TruckBrand)
                .Include(t => t.TruckColor)
                .Include(t => t.TruckCondition)
                .Include(t => t.TruckFuelType)
                .Include(t => t.TruckMileage)
                .Include(t => t.TruckModel)
                .Include(t => t.TruckTransmissionType)
                .Include(t => t.TruckVersion)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // GET: Trucks/Create
        public IActionResult Create()
        {
            ViewData["TruckBrandId"] = new SelectList(_context.TruckBrands, "Id", "BrandName");
            ViewData["TruckColorId"] = new SelectList(_context.TruckColors, "Id", "Color");
            ViewData["TruckConditionId"] = new SelectList(_context.TruckConditions, "Id", "Condition");
            ViewData["TruckFuelTypeId"] = new SelectList(_context.TruckFuelTypes, "Id", "FuelType");
            ViewData["TruckMileageId"] = new SelectList(_context.TruckMileages, "Id", "Mileage");
            ViewData["TruckModelId"] = new SelectList(_context.TruckModels, "Id", "ModelName");
            ViewData["TruckTransmissionTypeId"] = new SelectList(_context.TruckTransmissionTypes, "Id", "TransmissionType");
            ViewData["TruckVersionId"] = new SelectList(_context.TruckVersions, "Id", "VersionType");
            return View();
        }

        // POST: Trucks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, FirstRegistration, EnginePower, Price, Features, Description, Location, TruckBrandId, TruckModelId, TruckFuelTypeId, TruckColorId, TruckConditionId, TruckMileageId, TruckLoadCapacity, TruckTransmissionTypeId, TruckVersionId")] Truck truck, List<IFormFile> truckPhotos)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            truck.User = currentUser;

            if (truckPhotos != null && truckPhotos.Count > 0)
            {
                truck.TruckPhotos = new List<TruckPhoto>();

                foreach (var photo in truckPhotos)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await photo.CopyToAsync(memoryStream);
                        truck.TruckPhotos.Add(new TruckPhoto
                        {
                            PhotoData = memoryStream.ToArray(),
                            ContentType = photo.ContentType
                        });
                    }
                }
            }
            _context.Add(truck);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
        // GET: Trucks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Truck == null)
            {
                return NotFound();
            }

            var truck = await _context.Truck.FindAsync(id);
            if (truck == null)
            {
                return NotFound();
            }
            ViewData["TruckBrandId"] = new SelectList(_context.TruckBrands, "Id", "Id", truck.TruckBrandId);
            ViewData["TruckColorId"] = new SelectList(_context.TruckColors, "Id", "Id", truck.TruckColorId);
            ViewData["TruckConditionId"] = new SelectList(_context.TruckConditions, "Id", "Id", truck.TruckConditionId);
            ViewData["TruckFuelTypeId"] = new SelectList(_context.TruckFuelTypes, "Id", "Id", truck.TruckFuelTypeId);
            ViewData["TruckMileageId"] = new SelectList(_context.TruckMileages, "Id", "Id", truck.TruckMileageId);
            ViewData["TruckModelId"] = new SelectList(_context.TruckModels, "Id", "Id", truck.TruckModelId);
            ViewData["TruckTransmissionTypeId"] = new SelectList(_context.TruckTransmissionTypes, "Id", "Id", truck.TruckTransmissionTypeId);
            ViewData["TruckVersionId"] = new SelectList(_context.TruckVersions, "Id", "Id", truck.TruckVersionId);
            return View(truck);
        }


        // POST: Trucks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("\"Id,FirstRegistration,EnginePower,Price,Features,Description,Location,TruckBrandId,TruckModelId,TruckFuelTypeId,TruckColorId,TruckConditionId,TruckMileageId,TruckLoadCapacity,TruckTransmissionTypeId,TruckVersionId\"")] Truck truck)
        {
            if (id != truck.Id)
            {
                return NotFound();
            }


            try
            {
                _context.Update(truck);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckExists(truck.Id))
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

        // GET: Trucks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Truck == null)
            {
                return NotFound();
            }

            var truck = await _context.Truck
                .Include(t => t.TruckBrand)
                .Include(t => t.TruckColor)
                .Include(t => t.TruckCondition)
                .Include(t => t.TruckFuelType)
                .Include(t => t.TruckMileage)
                .Include(t => t.TruckModel)
                .Include(t => t.TruckTransmissionType)
                .Include(t => t.TruckVersion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // POST: Trucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Truck == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Truck'  is null.");
            }
            var truck = await _context.Truck.FindAsync(id);
            if (truck != null)
            {
                _context.Truck.Remove(truck);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckExists(int id)
        {
          return (_context.Truck?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
