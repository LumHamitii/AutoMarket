using Microsoft.AspNetCore.Identity;

namespace AutoMarket.Models
{
    public class Truck
    {
        public int Id { get; set; }

        public DateTime FirstRegistration { get; set; }

        public int EnginePower { get; set; }

        public float Price { get; set; }

        public string Features { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public int TruckBrandId { get; set; }
        public int TruckModelId { get; set; }
        public int TruckFuelTypeId { get; set; }
        public int TruckColorId { get; set; }
        public int TruckConditionId { get; set; }
        public int TruckMileageId { get; set; }
        public int TruckLoadCapacity { get; set; }
        public int TruckTransmissionTypeId { get; set; }
        public int TruckVersionId { get; set; }

      
        public IdentityUser User { get; set; }

        public TruckBrand TruckBrand { get; set; }
        public TruckModel TruckModel { get; set; }
        public TruckCondition TruckCondition { get; set; }
        public TruckColor TruckColor { get; set; }
        public TruckFuelType TruckFuelType { get; set; }
        public TruckMileage TruckMileage { get; set; } 
        public TruckTransmissionType TruckTransmissionType { get; set; }
        public TruckVersion TruckVersion { get; set; }
        public List<TruckPhoto> TruckPhotos { get; set; } 
    }
}
