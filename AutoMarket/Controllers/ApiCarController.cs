using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMarket.Data;
using AutoMarket.Models;

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
           .ToListAsync();
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/ApiiCar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/ApiiCar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar([FromBody] CarApiInputModel carApiInputModel)
        {
            try
            {
                // Map properties from input model to the carapi entity
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

                // Add and save the carapi
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Getcar", new { id = car.Id }, car);
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
