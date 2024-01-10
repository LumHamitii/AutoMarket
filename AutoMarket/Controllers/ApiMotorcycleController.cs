using System;
using System.Collections.Generic;
using System.IO;
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
    public class ApiMotorcycleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiMotorcycleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motorcycle>>> GetMotorcycles()
        {
            if (_context.Motorcycles == null)
            {
                return NotFound();
            }

            var motorcycles = await _context.Motorcycles
                .Include(m => m.MotorcycleBrand)
                .Include(m => m.MotorcycleColor)
                .Include(m => m.MotorcycleCondition)
                .Include(m => m.MotorcycleFuelType)
                .Include(m => m.MotorcycleMileage)
                .Include(m => m.MotorcycleModel)
                .Include(m => m.MotorcycleTransmission)
                .Include(m => m.MotorcycleType)
                .Include(m => m.MotorcycleYear)
                .ToListAsync();

            return motorcycles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Motorcycle>> GetMotorcycle(int id)
        {
            if (_context.Motorcycles == null)
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
                .Include(m => m.MotorcycleYear)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorcycle == null)
            {
                return NotFound();
            }

            return motorcycle;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotorcycle(int id, Motorcycle motorcycle)
        {
            if (id != motorcycle.Id)
            {
                return BadRequest();
            }

            _context.Entry(motorcycle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotorcycleExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Motorcycle>> PostMotorcycle([FromForm] MotorcycleApiInputModel motorcycleApiInputModel)
        {
            try
            {
                Debug.WriteLine($"Received request with {motorcycleApiInputModel.Files.Count} files.");

                var motorcycle = new Motorcycle
                {
                    FirstRegistration = motorcycleApiInputModel.FirstRegistration,
                    EnginePower = motorcycleApiInputModel.EnginePower,
                    Price = motorcycleApiInputModel.Price,
                    Description = motorcycleApiInputModel.Description,
                    MotorcycleBrandId = motorcycleApiInputModel.MotorcycleBrandId,
                    MotorcycleModelId = motorcycleApiInputModel.MotorcycleModelId,
                    MotorcycleFuelTypeId = motorcycleApiInputModel.MotorcycleFuelTypeId,
                    MotorcycleColorId = motorcycleApiInputModel.MotorcycleColorId,
                    MotorcycleConditionId = motorcycleApiInputModel.MotorcycleConditionId,
                    MotorcycleMileageId = motorcycleApiInputModel.MotorcycleMileageId,
                    MotorcycleTransmissionId = motorcycleApiInputModel.MotorcycleTransmissionId,
                    MotorcycleTypeId = motorcycleApiInputModel.MotorcycleTypeId,
                    MotorcycleYearId = motorcycleApiInputModel.MotorcycleYearId,
                    MotorcyclePhotos = new List<MotorcyclePhoto>()
                };

                motorcycle.MotorcycleBrand = await _context.MotorcycleBrands.FindAsync(motorcycle.MotorcycleBrandId);
                motorcycle.MotorcycleModel = await _context.MotorcycleModels.FindAsync(motorcycle.MotorcycleModelId);
                motorcycle.MotorcycleFuelType = await _context.MotorcycleFuelTypes.FindAsync(motorcycle.MotorcycleFuelTypeId);
                motorcycle.MotorcycleColor = await _context.MotorcycleColors.FindAsync(motorcycle.MotorcycleColorId);
                motorcycle.MotorcycleCondition = await _context.MotorcycleConditions.FindAsync(motorcycle.MotorcycleConditionId);
                motorcycle.MotorcycleMileage = await _context.MotorcycleMileages.FindAsync(motorcycle.MotorcycleMileageId);
                motorcycle.MotorcycleTransmission = await _context.MotorcycleTransmissions.FindAsync(motorcycle.MotorcycleTransmissionId);
                motorcycle.MotorcycleType = await _context.MotorcycleTypes.FindAsync(motorcycle.MotorcycleTypeId);
                motorcycle.MotorcycleYear = await _context.MotorcycleYears.FindAsync(motorcycle.MotorcycleYearId);

                if (motorcycle.MotorcycleBrand == null || motorcycle.MotorcycleModel == null || motorcycle.MotorcycleFuelType == null ||
                    motorcycle.MotorcycleColor == null || motorcycle.MotorcycleCondition == null || motorcycle.MotorcycleMileage == null ||
                    motorcycle.MotorcycleTransmission == null || motorcycle.MotorcycleType == null || motorcycle.MotorcycleYear == null)
                {
                    return NotFound("One or more related entities not found.");
                }

                foreach (var file in motorcycleApiInputModel.Files)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);

                            var motorcyclePhoto = new MotorcyclePhoto
                            {
                                PhotoData = ms.ToArray(),
                                ContentType = file.ContentType
                                // Set other properties as needed (e.g., caption, description, etc.)
                            };

                            motorcycle.MotorcyclePhotos.Add(motorcyclePhoto);
                        }
                    }
                }

                _context.Motorcycles.Add(motorcycle);
                await _context.SaveChangesAsync();
                Debug.WriteLine($"Created motorcycle with ID: {motorcycle.Id}");

                return CreatedAtAction("GetMotorcycle", new { id = motorcycle.Id }, motorcycle);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorcycle(int id)
        {
            if (_context.Motorcycles == null)
            {
                return NotFound();
            }

            var motorcycle = await _context.Motorcycles.FindAsync(id);
            if (motorcycle == null)
            {
                return NotFound();
            }

            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MotorcycleExists(int id)
        {
            return (_context.Motorcycles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
