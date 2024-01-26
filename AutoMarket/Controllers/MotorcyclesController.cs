using AutoMarket.Data;
using AutoMarket.Models;
using AutoMarket.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AutoMarket.Controllers
{
    public class MotorcyclesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MotorcyclesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Motorcycles
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 4;
            var motorcycles = await _context.Motorcycles
                .Include(m => m.MotorcycleBrand)
                .Include(m => m.MotorcycleColor)
                .Include(m => m.MotorcycleCondition)
                .Include(m => m.MotorcycleFuelType)
                .Include(m => m.MotorcycleMileage)
                .Include(m => m.MotorcycleModel)
                .Include(m => m.MotorcycleTransmission)
                .Include(m => m.MotorcycleType)
                 .Include(m => m.MotorcyclePhotos)
                .ToPagedListAsync(pageNumber, pageSize);

            return View(motorcycles);
        }

        // GET: Motorcycles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Motorcycles == null)
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

            return View(motorcycle);
        }

        // GET: Motorcycles/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.MotorcycleBrandId = new SelectList(_context.MotorcycleBrands, "Id", "BrandName");
            ViewBag.MotorcycleColorId = new SelectList(_context.MotorcycleColors, "Id", "Color");
            ViewBag.MotorcycleConditionId = new SelectList(_context.MotorcycleConditions, "Id", "Condition");
            ViewBag.MotorcycleFuelTypeId = new SelectList(_context.MotorcycleFuelTypes, "Id", "Fuel");
            ViewBag.MotorcycleMileageId = new SelectList(_context.MotorcycleMileages, "Id", "Mileage");
            ViewBag.MotorcycleModelId = new SelectList(_context.MotorcycleModels, "Id", "ModelName");
            ViewBag.MotorcycleTransmissionId = new SelectList(_context.MotorcycleTransmissions, "Id", "Transmission");
            ViewBag.MotorcycleTypeId = new SelectList(_context.MotorcycleTypes, "Id", "Type");
            return View();
        }

        // POST: Motorcycles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstRegistration,EnginePower,Price,Description,Location,MotorcycleBrandId,MotorcycleModelId,MotorcycleTypeId,MotorcycleColorId,MotorcycleMileageId,MotorcycleConditionId,MotorcycleTransmissionId,MotorcycleFuelTypeId")] Motorcycle motorcycle, List<IFormFile> motorcyclePhotos)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            motorcycle.User = currentUser;

            if (motorcyclePhotos != null && motorcyclePhotos.Count > 0)
            {
                motorcycle.MotorcyclePhotos = new List<MotorcyclePhoto>();

                foreach (var photo in motorcyclePhotos)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await photo.CopyToAsync(memoryStream);
                        motorcycle.MotorcyclePhotos.Add(new MotorcyclePhoto
                        {
                            PhotoData = memoryStream.ToArray(),
                            ContentType = photo.ContentType
                        });
                    }
                }
            }
            _context.Add(motorcycle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        // GET: Motorcycles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Motorcycles == null)
            {
                return NotFound();
            }

            var motorcycle = await _context.Motorcycles.FindAsync(id);
            if (motorcycle == null)
            {
                return NotFound();
            }

            ViewBag.MotorcycleBrandId = new SelectList(_context.MotorcycleBrands, "Id", "BrandName", motorcycle.MotorcycleBrandId);
            ViewBag.MotorcycleColorId = new SelectList(_context.MotorcycleColors, "Id", "Color", motorcycle.MotorcycleColorId);
            ViewBag.MotorcycleConditionId = new SelectList(_context.MotorcycleConditions, "Id", "Condition", motorcycle.MotorcycleConditionId);
            ViewBag.MotorcycleFuelTypeId = new SelectList(_context.MotorcycleFuelTypes, "Id", "Fuel", motorcycle.MotorcycleFuelTypeId);
            ViewBag.MotorcycleMileageId = new SelectList(_context.MotorcycleMileages, "Id", "Mileage", motorcycle.MotorcycleMileageId);
            ViewBag.MotorcycleModelId = new SelectList(_context.MotorcycleModels, "Id", "ModelName", motorcycle.MotorcycleModelId);
            ViewBag.MotorcycleTransmissionId = new SelectList(_context.MotorcycleTransmissions, "Id", "Transmission", motorcycle.MotorcycleTransmissionId);
            ViewBag.MotorcycleTypeId = new SelectList(_context.MotorcycleTypes, "Id", "Type", motorcycle.MotorcycleTypeId);

            return View(motorcycle);
        }

        // POST: Motorcycles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstRegistration,EnginePower,Price,Description,Location,MotorcycleBrandId,MotorcycleModelId,MotorcycleTypeId,MotorcycleColorId,MotorcycleMileageId,MotorcycleConditionId,MotorcycleTransmissionId,MotorcycleFuelTypeId")] Motorcycle motorcycle)
        {
            if (id != motorcycle.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(motorcycle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotorcycleExists(motorcycle.Id))
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
        // GET: Motorcycles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Motorcycles == null)
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

            return View(motorcycle);
        }

        // POST: Motorcycles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motorcycle = await _context.Motorcycles.FindAsync(id);

            if (motorcycle == null)
            {
                return NotFound();
            }

            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> FilterMotorcycles(
           [FromQuery] int? brandId,
           [FromQuery] int? modelId,
           [FromQuery] int? fuelTypeId,
           [FromQuery] int? colorId,
           [FromQuery] int? conditionId,
           [FromQuery] int? mileageId,
           [FromQuery] int? transmissionId,
           [FromQuery] int? typeId,
           [FromQuery] DateTime? startDate,
           [FromQuery] DateTime? endDate,
           [FromQuery] string sortByPrice,
           [FromQuery] float? minPrice,
           [FromQuery] float? maxPrice,
           [FromQuery] bool? isNew,
           [FromQuery] bool? isUsed)

        {
            IQueryable<Motorcycle> query = _context.Motorcycles
                .Include(m => m.MotorcycleBrand)
                .Include(m => m.MotorcycleModel)
                .Include(m => m.MotorcycleFuelType)
                .Include(m => m.MotorcycleColor)
                .Include(m => m.MotorcycleCondition)
                .Include(m => m.MotorcycleMileage)
                .Include(m => m.MotorcycleTransmission)
                .Include(m => m.MotorcycleType)
                .Include(m => m.MotorcyclePhotos)
                .Include(m => m.User);

            if (brandId.HasValue)
            {
                query = query.Where(m => m.MotorcycleBrandId == brandId.Value);
            }

            if (modelId.HasValue)
            {
                query = query.Where(m => m.MotorcycleModelId == modelId.Value);
            }

            if (fuelTypeId.HasValue)
            {
                query = query.Where(m => m.MotorcycleFuelTypeId == fuelTypeId.Value);
            }

            if (colorId.HasValue)
            {
                query = query.Where(m => m.MotorcycleColorId == colorId.Value);
            }

            if (mileageId.HasValue)
            {
                query = query.Where(m => m.MotorcycleMileageId == mileageId.Value);
            }

            if (transmissionId.HasValue)
            {
                query = query.Where(m => m.MotorcycleTransmissionId == transmissionId.Value);
            }

            if (typeId.HasValue)
            {
                query = query.Where(m => m.MotorcycleTypeId == typeId.Value);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(m => m.FirstRegistration >= startDate.Value && m.FirstRegistration <= endDate.Value);
            }
            
             if (minPrice.HasValue)
            {
                query = query.Where(m => m.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(m => m.Price <= maxPrice.Value);
            }

            
            switch (sortByPrice)
            {
                case "lowToHigh":
                    query = query.OrderBy(m => m.Price);
                    break;
                case "highToLow":
                    query = query.OrderByDescending(m => m.Price);
                    break;

                default:
                    break;
            }
            
            if (isNew.HasValue && isNew.Value)
            {

                query = query.Where(m => m.MotorcycleCondition.Condition == "New");
            }

            if (isUsed.HasValue && isUsed.Value)
            {

                query = query.Where(m => m.MotorcycleCondition.Condition == "Used");
            }
             

            var filteredMotorcycles = await query.ToListAsync();

            var viewModel = new FilterMotorcyclesViewModel
            {
                Brands = await _context.MotorcycleBrands.ToListAsync(),
                Models = await _context.MotorcycleModels.ToListAsync(),
                FuelTypes = await _context.MotorcycleFuelTypes.ToListAsync(),
                Colors = await _context.MotorcycleColors.ToListAsync(),
                Conditions = await _context.MotorcycleConditions.ToListAsync(),
                Mileages = await _context.MotorcycleMileages.ToListAsync(),
                Transmissions = await _context.MotorcycleTransmissions.ToListAsync(),
                Types = await _context.MotorcycleTypes.ToListAsync(),
                FilteredMotorcycles = filteredMotorcycles
            };

            return View(viewModel);
        }
        private bool MotorcycleExists(int id)
        {
            return (_context.Motorcycles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}