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

        // GET: api/ApiCar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
          if (_context.Cars == null)
          {
              return NotFound();
          }
            return await _context.Cars.ToListAsync();
        }

        // GET: api/ApiCar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
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

            return car;
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
        // PUT: api/ApiCar/5
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

        // POST: api/ApiCar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
          if (_context.Cars == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
          }
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/ApiCar/5
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
