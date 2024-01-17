using AutoMarket.Models;
using System;
using System.Collections.Generic;
namespace AutoMarket.ViewModel
{
    public class FilterTrucksViewModel
    {
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public int? FuelTypeId { get; set; }
        public int? ColorId { get; set; }
        public int? ConditionId { get; set; }
        public int? MileageId { get; set; }
        public int? TransmissionId { get; set; }
        public int? VersionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<Truck> FilteredTrucks { get; set; }

        public List<TruckBrand> Brands { get; set; }
        public List<TruckModel> Models { get; set; }
        public List<TruckFuelType> FuelTypes { get; set; }
        public List<TruckColor> Colors { get; set; }
        public List<TruckCondition> Conditions { get; set; }
        public List<TruckMileage> Mileages { get; set; }
        public List<TruckTransmissionType> Transmissions { get; set; }
        public List<TruckVersion> Versions { get; set; }
    }
}
