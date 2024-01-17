using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMarket.Data;
using AutoMarket.Models;
using System.Diagnostics;

namespace AutoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTruckController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiTruckController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Truck>>> GetTrucks()
        {
            if (_context.Truck == null)
            {
                return NotFound();
            }

            var trucks = await _context.Truck
                .Include(t => t.TruckBrand)
                .Include(t => t.TruckColor)
                .Include(t => t.TruckCondition)
                .Include(t => t.TruckFuelType)
                .Include(t => t.TruckMileage)
                .Include(t => t.TruckModel)
                .Include(t => t.TruckTransmissionType)
                .Include(t => t.TruckVersion)
                .Include(t => t.TruckPhotos)
                .ToListAsync();

            return trucks;
        }
        [HttpGet("GetTruckBrands")]
        public async Task<ActionResult<IEnumerable<TruckBrand>>> GetTruckBrands()
        {
            var truckBrands = await _context.TruckBrands.ToListAsync();
            return truckBrands;
        }
        [HttpGet("GetTruckColors")]
        public async Task<ActionResult<IEnumerable<TruckColor>>> GetTruckColors()
        {
            var truckColors = await _context.TruckColors.ToListAsync();
            return truckColors;
        }
        [HttpGet("GetTruckConditions")]
        public async Task<ActionResult<IEnumerable<TruckCondition>>> GetTruckConditions()
        {
            var truckConditions = await _context.TruckConditions.ToListAsync();
            return truckConditions;
        }
        [HttpGet("GetTruckFuelTypes")]
        public async Task<ActionResult<IEnumerable<TruckFuelType>>> GetTruckFuelTypes()
        {
            var truckfuelTypes = await _context.TruckFuelTypes.ToListAsync();
            return truckfuelTypes;
        }
        [HttpGet("GetTruckMileages")]
        public async Task<ActionResult<IEnumerable<TruckMileage>>> GetTruckMileages()
        {
            var truckMileages = await _context.TruckMileages.ToListAsync();
            return truckMileages;
        }
        [HttpGet("GetTruckModels")]
        public async Task<ActionResult<IEnumerable<TruckModel>>> GetTruckModels()
        {
            var truckModels = await _context.TruckModels.ToListAsync();
            return truckModels;
        }
        [HttpGet("GetTruckTransmissionTypes")]
        public async Task<ActionResult<IEnumerable<TruckTransmissionType>>> GetTruckTransmissionTypes()
        {
            var truckTransmissionTypes = await _context.TruckTransmissionTypes.ToListAsync();
            return truckTransmissionTypes;
        }
        [HttpGet("GetTruckVersions")]
        public async Task<ActionResult<IEnumerable<TruckVersion>>> GetTruckVersions()
        {
            var truckVersions = await _context.TruckVersions.ToListAsync();
            return truckVersions;
        }


        // GET: api/ApiTruck/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Truck>> GetTruck(int id)
        {
            if (_context.Truck == null)
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
                .Include(t => t.TruckPhotos)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (truck == null)
            {
                return NotFound();
            }

            return truck;
        }

        // PUT: api/ApiTruck/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTruck(int id, Truck truck)
        {
            if (id != truck.Id)
            {
                return BadRequest();
            }

            _context.Entry(truck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiTruck
         [HttpPost]
        public async Task<ActionResult<Truck>> PostTruck([FromForm] TruckApiInputModel truckApiInputModel)
        {
            try
            {
                Debug.WriteLine($"Received request with {truckApiInputModel.Files.Count} files.");

                var truck = new Truck
                {
                    FirstRegistration = truckApiInputModel.FirstRegistration,
                    EnginePower = truckApiInputModel.EnginePower,
                    Price = truckApiInputModel.Price,
                    Features = truckApiInputModel.Features,
                    Description = truckApiInputModel.Description,
                    Location = truckApiInputModel.Location,
                    TruckBrandId = truckApiInputModel.TruckBrandId,
                    TruckModelId = truckApiInputModel.TruckModelId,
                    TruckFuelTypeId = truckApiInputModel.TruckFuelTypeId,
                    TruckColorId = truckApiInputModel.TruckColorId,
                    TruckConditionId = truckApiInputModel.TruckConditionId,
                    TruckMileageId = truckApiInputModel.TruckMileageId,
                    TruckLoadCapacity = truckApiInputModel.TruckLoadCapacity,
                    TruckTransmissionTypeId = truckApiInputModel.TruckTransmissionTypeId,
                    TruckVersionId = truckApiInputModel.TruckVersionId,
                    TruckPhotos = new List<TruckPhoto>(),
                    UserId = truckApiInputModel.UserId
                };

                truck.TruckBrand = await _context.TruckBrands.FindAsync(truck.TruckBrandId);
                truck.TruckModel = await _context.TruckModels.FindAsync(truck.TruckModelId);
                truck.TruckFuelType = await _context.TruckFuelTypes.FindAsync(truck.TruckFuelTypeId);
                truck.TruckColor = await _context.TruckColors.FindAsync(truck.TruckColorId);
                truck.TruckCondition = await _context.TruckConditions.FindAsync(truck.TruckConditionId);
                truck.TruckMileage = await _context.TruckMileages.FindAsync(truck.TruckMileageId);
                truck.TruckTransmissionType = await _context.TruckTransmissionTypes.FindAsync(truck.TruckTransmissionTypeId);
                truck.TruckVersion = await _context.TruckVersions.FindAsync(truck.TruckVersionId);

                if (truck.TruckBrand == null || truck.TruckModel == null || truck.TruckFuelType == null || truck.TruckColor == null ||
                    truck.TruckCondition == null || truck.TruckMileage == null ||
                    truck.TruckTransmissionType == null || truck.TruckVersion == null)
                {
                    return NotFound("One or more related entities not found.");
                }

                foreach (var file in truckApiInputModel.Files)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);

                            var truckPhoto = new TruckPhoto
                            {
                                PhotoData = ms.ToArray(),
                                ContentType = file.ContentType
                            };

                            truck.TruckPhotos.Add(truckPhoto);
                        }
                    }
                }

                _context.Truck.Add(truck);
                await _context.SaveChangesAsync();
                Debug.WriteLine($"Created truck with ID: {truck.Id}");

                return CreatedAtAction("GetTruck", new { id = truck.Id }, truck);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Truck>>> FilterTrucks(
    [FromQuery] int? brandId,
    [FromQuery] int? modelId,
    [FromQuery] int? fuelTypeId,
    [FromQuery] int? colorId,
    [FromQuery] int? conditionId,
    [FromQuery] int? mileageId,
    [FromQuery] int? loadCapacity,
    [FromQuery] int? transmissionTypeId,
    [FromQuery] int? versionId,
    [FromQuery] DateTime? startDate,
    [FromQuery] DateTime? endDate)
        {
            IQueryable<Truck> query = _context.Truck
                .Include(t => t.TruckBrand)
                .Include(t => t.TruckModel)
                .Include(t => t.TruckFuelType)
                .Include(t => t.TruckColor)
                .Include(t => t.TruckCondition)
                .Include(t => t.TruckMileage)
                .Include(t => t.TruckTransmissionType)
                .Include(t => t.TruckVersion)
                .Include(t => t.TruckPhotos);

            if (brandId.HasValue)
            {
                query = query.Where(t => t.TruckBrandId == brandId.Value);
            }

            if (modelId.HasValue)
            {
                query = query.Where(t => t.TruckModelId == modelId.Value);
            }

            if (fuelTypeId.HasValue)
            {
                query = query.Where(t => t.TruckFuelTypeId == fuelTypeId.Value);
            }

            if (colorId.HasValue)
            {
                query = query.Where(t => t.TruckColorId == colorId.Value);
            }

            if (conditionId.HasValue)
            {
                query = query.Where(t => t.TruckConditionId == conditionId.Value);
            }

            if (mileageId.HasValue)
            {
                query = query.Where(t => t.TruckMileageId == mileageId.Value);
            }

            if (loadCapacity.HasValue)
            {
                query = query.Where(t => t.TruckLoadCapacity == loadCapacity.Value);
            }

            if (transmissionTypeId.HasValue)
            {
                query = query.Where(t => t.TruckTransmissionTypeId == transmissionTypeId.Value);
            }

            if (versionId.HasValue)
            {
                query = query.Where(t => t.TruckVersionId == versionId.Value);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(t => t.FirstRegistration >= startDate.Value && t.FirstRegistration <= endDate.Value);
            }

            var filteredTrucks = await query.ToListAsync();

            return Ok(filteredTrucks);
        }

        // DELETE: api/ApiTruck/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            if (_context.Truck == null)
            {
                return NotFound();
            }
            var truck = await _context.Truck.FindAsync(id);
            if (truck == null)
            {
                return NotFound();
            }

            _context.Truck.Remove(truck);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TruckExists(int id)
        {
            return (_context.Truck?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}