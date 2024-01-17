namespace AutoMarket.Models
{
    public class TruckApiInputModel
    {
        public DateTime FirstRegistration { get; set; }

        public int EnginePower { get; set; }

        public float Price { get; set; }

        public string Features { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
        public string UserId { get; set; }
        public int TruckBrandId { get; set; }
        public int TruckModelId { get; set; }
        public int TruckFuelTypeId { get; set; }
        public int TruckColorId { get; set; }
        public int TruckConditionId { get; set; }
        public int TruckMileageId { get; set; }
        public int TruckLoadCapacity { get; set; }
        public int TruckTransmissionTypeId { get; set; }
        public int TruckVersionId { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
