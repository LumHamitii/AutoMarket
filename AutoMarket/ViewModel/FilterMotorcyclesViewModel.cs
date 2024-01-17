using AutoMarket.Models;
using System;
using System.Collections.Generic;

namespace AutoMarket.ViewModel
{
    public class FilterMotorcyclesViewModel
    {
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public int? FuelTypeId { get; set; }
        public int? ColorId { get; set; }
        public int? ConditionId { get; set; }
        public int? MileageId { get; set; }
        public int? TransmissionId { get; set; }
        public int? TypeId { get; set; }
        public int? YearId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<Motorcycle> FilteredMotorcycles { get; set; }

        public List<MotorcycleBrand> Brands { get; set; }
        public List<MotorcycleModel> Models { get; set; }
        public List<MotorcycleFuelType> FuelTypes { get; set; }
        public List<MotorcycleColor> Colors { get; set; }
        public List<MotorcycleCondition> Conditions { get; set; }
        public List<MotorcycleMileage> Mileages { get; set; }
        public List<MotorcycleTransmission> Transmissions { get; set; }
        public List<MotorcycleType> Types { get; set; }
    }
}
