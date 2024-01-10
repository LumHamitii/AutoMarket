namespace AutoMarket.Models
{
    public class MotorcycleApiInputModel
    {
        public DateTime FirstRegistration { get; set; }

        public int EnginePower { get; set; }

        public float Price { get; set; }
        public string Description { get; set; }

        public int MotorcycleBrandId { get; set; }

        public int MotorcycleModelId { get; set; }

        public int MotorcycleYearId { get; set; }

        public int MotorcycleTypeId { get; set; }

        public int MotorcycleColorId { get; set; }

        public int MotorcycleMileageId { get; set; }

        public int MotorcycleConditionId { get; set; }

        public int MotorcycleTransmissionId { get; set; }

        public int MotorcycleFuelTypeId { get; set; }
        public List<IFormFile> Files { get; set; }

    }
}
