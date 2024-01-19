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
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace AutoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCarController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ApiCarController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: api/ApiiCar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
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
                 .Include(c => c.Photos)
         .ToListAsync();
            return await _context.Cars.ToListAsync();
        }
        [HttpGet("GetCarBrands")]
        public async Task<ActionResult<IEnumerable<CarBrand>>> GetCarBrands()
        {
            var carBrands = await _context.Brands.ToListAsync();
            return carBrands;
        }

        // GET: api/ApiCar/GetCarModels
        [HttpGet("GetCarModels")]
        public async Task<ActionResult<IEnumerable<CarModel>>> GetCarModels()
        {
            var carModels = await _context.Models.ToListAsync();
            return carModels;
        }

        // GET: api/ApiCar/GetCarConditions
        [HttpGet("GetCarConditions")]
        public async Task<ActionResult<IEnumerable<CarCondition>>> GetCarConditions()
        {
            var carConditions = await _context.Condition.ToListAsync();
            return carConditions;
        }
        [HttpGet("GetCarColors")]
        public async Task<ActionResult<IEnumerable<CarColor>>> GetCarColors()
        {
            var carColors = await _context.Colors.ToListAsync();
            return carColors;
        }
        [HttpGet("GetCarFuelTypes")]
        public async Task<ActionResult<IEnumerable<CarFuelType>>> GetCarFuelTypes()
        {
            var carFuelTypes = await _context.FuelTypes.ToListAsync();
            return carFuelTypes;
        }
        [HttpGet("GetCarMileages")]
        public async Task<ActionResult<IEnumerable<CarMileage>>> GetCarMileages()
        {
            var carMileages = await _context.Mileages.ToListAsync();
            return carMileages;
        }
        [HttpGet("GetCarSeats")]
        public async Task<ActionResult<IEnumerable<CarSeats>>> GetCarSeats()
        {
            var carSeats = await _context.Seats.ToListAsync();
            return carSeats;
        }
        [HttpGet("GetCarTransmissionTypes")]
        public async Task<ActionResult<IEnumerable<CarTransmissionType>>> GetCarTransmissionTypes()
        {
            var carTransmissionTypes = await _context.TransmissionTypes.ToListAsync();
            return carTransmissionTypes;
        }
        [HttpGet("GetCarVersions")]
        public async Task<ActionResult<IEnumerable<CarVersion>>> GetCarVersions()
        {
            var carVersions = await _context.Versions.ToListAsync();
            return carVersions;
        }

        // GET: api/ApiiCar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
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
                   .Include(c => c.Photos)
                   .Include(c => c.User)
           .ToListAsync();
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Car>>> FilterCars(
    [FromQuery] int? brandId,
    [FromQuery] int? modelId,
    [FromQuery] int? fuelTypeId,
    [FromQuery] int? colorId,
    [FromQuery] int? conditionId,
    [FromQuery] int? mileageId,
    [FromQuery] int? seatsId,
    [FromQuery] int? transmissionTypeId,
    [FromQuery] int? versionId,
    [FromQuery] DateTime? startDate,
    [FromQuery] DateTime? endDate)

        {
            IQueryable<Car> query = _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarModel)
                .Include(c => c.CarFuelType)
                .Include(c => c.CarColor)
                .Include(c => c.CarCondition)
                .Include(c => c.CarMileage)
                .Include(c => c.CarSeats)
                .Include(c => c.CarTransmissionType)
                .Include(c => c.CarVersion)
                .Include(c => c.Photos);

            if (brandId.HasValue)
            {
                query = query.Where(c => c.CarBrandId == brandId.Value);
            }

            if (modelId.HasValue)
            {
                query = query.Where(c => c.CarModelId == modelId.Value);
            }

            if (fuelTypeId.HasValue)
            {
                query = query.Where(c => c.CarFuelTypeId == fuelTypeId.Value);
            }

            if (colorId.HasValue)
            {
                query = query.Where(c => c.CarColorId == colorId.Value);
            }

            if (conditionId.HasValue)
            {
                query = query.Where(c => c.CarConditionId == conditionId.Value);
            }

            if (mileageId.HasValue)
            {
                query = query.Where(c => c.CarMileageId == mileageId.Value);
            }

            if (seatsId.HasValue)
            {
                query = query.Where(c => c.CarSeatsId == seatsId.Value);
            }

            if (transmissionTypeId.HasValue)
            {
                query = query.Where(c => c.CarTransmissionTypeId == transmissionTypeId.Value);
            }

            if (versionId.HasValue)
            {
                query = query.Where(c => c.CarVersionId == versionId.Value);
            }
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(c => c.FirstRegistration >= startDate.Value && c.FirstRegistration <= endDate.Value);
            }
            var filteredCars = await query.ToListAsync();

            return Ok(filteredCars);
        }

        // PUT: api/ApiiCar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       

        // POST: api/ApiiCar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar([FromForm] CarApiInputModel carApiInputModel)
        {
            try
            {
                Debug.WriteLine($"Received request with {carApiInputModel.Files.Count} files.");
                // Map properties from input model to the car entity
                var car = new Car
                {
                    FirstRegistration = carApiInputModel.FirstRegistration,
                    EnginePower = carApiInputModel.EnginePower,
                    Price = carApiInputModel.Price,
                    Features = carApiInputModel.Features,
                    Description = carApiInputModel.Description,
                    Location = carApiInputModel.Location,
                    CarBrandId = carApiInputModel.CarBrandId,
                    CarModelId = carApiInputModel.CarModelId,
                    CarFuelTypeId = carApiInputModel.CarFuelTypeId,
                    CarColorId = carApiInputModel.CarColorId,
                    CarConditionId = carApiInputModel.CarConditionId,
                    CarMileageId = carApiInputModel.CarMileageId,
                    CarSeatsId = carApiInputModel.CarSeatsId,
                    CarTransmissionTypeId = carApiInputModel.CarTransmissionTypeId,
                    CarVersionId = carApiInputModel.CarVersionId,
                    Photos = new List<CarPhoto>(),
                   UserId = carApiInputModel.UserId
                };

                // Fetch related entities from the database based on IDs
                car.CarBrand = await _context.Brands.FindAsync(car.CarBrandId);
                car.CarModel = await _context.Models.FindAsync(car.CarModelId);
                car.CarFuelType = await _context.FuelTypes.FindAsync(car.CarFuelTypeId);
                car.CarColor = await _context.Colors.FindAsync(car.CarColorId);
                car.CarCondition = await _context.Condition.FindAsync(car.CarConditionId);
                car.CarMileage = await _context.Mileages.FindAsync(car.CarMileageId);
                car.CarSeats = await _context.Seats.FindAsync(car.CarSeatsId);
                car.CarTransmissionType = await _context.TransmissionTypes.FindAsync(car.CarTransmissionTypeId);
                car.CarVersion = await _context.Versions.FindAsync(car.CarVersionId);

                // Check if any of the entities is not found
                if (car.CarBrand == null || car.CarModel == null || car.CarFuelType == null || car.CarColor == null ||
                    car.CarCondition == null || car.CarMileage == null || car.CarSeats == null ||
                    car.CarTransmissionType == null || car.CarVersion == null)
                {
                    return NotFound("One or more related entities not found.");
                }

                foreach (var file in carApiInputModel.Files)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Read the file content into a MemoryStream
                            await file.CopyToAsync(ms);

                            // Create a new CarPhoto entity for each file
                            var carPhoto = new CarPhoto
                            {
                                PhotoData = ms.ToArray(),
                                ContentType = file.ContentType
                                // Set other properties as needed (e.g., caption, description, etc.)
                            };

                            // Associate the CarPhoto with the Car
                            car.Photos.Add(carPhoto);
                        }
                    }
                }

                // Save the Car entity with the associated CarPhotos
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                Debug.WriteLine($"Created car with ID: {car.Id}");
                return CreatedAtAction("Getcar", new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        // PUT: api/ApiiCar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // PATCH: api/ApiiCar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, [FromForm] CarApiInputModel updatedCar)
        {
            if (updatedCar == null)
            {
                return BadRequest("Invalid request body.");
            }

            try
            {
                // Fetch the existing car entity from the database
                var existingCar = await _context.Cars
                    .Include(c => c.CarBrand)
                    .Include(c => c.CarColor)
                    .Include(c => c.CarCondition)
                    .Include(c => c.CarFuelType)
                    .Include(c => c.CarMileage)
                    .Include(c => c.CarModel)
                    .Include(c => c.CarSeats)
                    .Include(c => c.CarTransmissionType)
                    .Include(c => c.CarVersion)
                    .Include(c => c.Photos)
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (existingCar == null)
                {
                    return NotFound($"Car with ID {id} not found.");
                }

                existingCar.FirstRegistration = updatedCar.FirstRegistration;
                existingCar.EnginePower = updatedCar.EnginePower;
                existingCar.Price = updatedCar.Price;
                existingCar.Features = updatedCar.Features;
                existingCar.Description = updatedCar.Description;
                existingCar.Location = updatedCar.Location;
                existingCar.CarBrandId = updatedCar.CarBrandId;
                existingCar.CarModelId = updatedCar.CarModelId;
                existingCar.CarFuelTypeId = updatedCar.CarFuelTypeId;
                existingCar.CarColorId = updatedCar.CarColorId;
                existingCar.CarConditionId = updatedCar.CarConditionId;
                existingCar.CarMileageId = updatedCar.CarMileageId;
                existingCar.CarSeatsId = updatedCar.CarSeatsId;
                existingCar.CarTransmissionTypeId = updatedCar.CarTransmissionTypeId;
                existingCar.CarVersionId = updatedCar.CarVersionId;
                existingCar.UserId = updatedCar.UserId;

                existingCar.CarBrand = await _context.Brands.FindAsync(updatedCar.CarBrandId);
                existingCar.CarModel = await _context.Models.FindAsync(updatedCar.CarModelId);
                existingCar.CarFuelType = await _context.FuelTypes.FindAsync(updatedCar.CarFuelTypeId);
                existingCar.CarColor = await _context.Colors.FindAsync(updatedCar.CarColorId);
                existingCar.CarCondition = await _context.Condition.FindAsync(updatedCar.CarConditionId);
                existingCar.CarMileage = await _context.Mileages.FindAsync(updatedCar.CarMileageId);
                existingCar.CarSeats = await _context.Seats.FindAsync(updatedCar.CarSeatsId);
                existingCar.CarTransmissionType = await _context.TransmissionTypes.FindAsync(updatedCar.CarTransmissionTypeId);
                existingCar.CarVersion = await _context.Versions.FindAsync(updatedCar.CarVersionId);
                existingCar.User = await _context.Users.FindAsync(updatedCar.UserId);

                if (existingCar.CarBrand == null || existingCar.CarModel == null || existingCar.CarFuelType == null ||
                    existingCar.CarColor == null || existingCar.CarCondition == null || existingCar.CarMileage == null ||
                    existingCar.CarSeats == null || existingCar.CarTransmissionType == null || existingCar.CarVersion == null)
                {
                    return NotFound("One or more related entities not found.");
                }

                foreach (var file in updatedCar.Files)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);

                            var carPhoto = new CarPhoto
                            {
                                PhotoData = ms.ToArray(),
                                ContentType = file.ContentType
                            };

                            existingCar.Photos.Add(carPhoto);
                        }
                    }
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        // DELETE: api/ApiiCar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
