using AutoMarket.Models;
using System.Drawing;

namespace AutoMarket.ViewModel
{
    public class FilterCarsViewModel
    {
        public float? Price { get; set; }
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public int? FuelTypeId { get; set; }
        public int? ColorId { get; set; }
        public int? MileageId { get; set; }
        public int? SeatsId { get; set; }
        public int? TransmissionTypeId { get; set; }
        public int? VersionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SortByPrice { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public bool IsNew { get; set; }
        public bool IsUsed { get; set; }
        public List<Car> FilteredCars { get; set; }

        // Lists for dropdowns
        public List<CarBrand> Brands { get; set; }
        public List<CarModel> Models { get; set; }
        public List<CarFuelType> FuelTypes { get; set; }
        public List<CarColor> Colors { get; set; }
        public List<CarCondition> Conditions { get; set; }
        public List<CarMileage> Mileages { get; set; }
        public List<CarSeats> Seats { get; set; }
        public List<CarTransmissionType> TransmissionTypes { get; set; }
        public List<CarVersion> Versions { get; set; }
    }
}
