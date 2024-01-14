namespace AutoMarket.Models
{
    public class CarApiInputModel
    {
        public DateTime FirstRegistration { get; set; }
        public int EnginePower { get; set; }
        public float Price { get; set; }
        public string Features { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }

        public int CarBrandId { get; set; }
        public int CarModelId { get; set; }
        public int CarFuelTypeId { get; set; }
        public int CarColorId { get; set; }
        public int CarConditionId { get; set; }
        public int CarMileageId { get; set; }
        public int CarSeatsId { get; set; }
        public int CarTransmissionTypeId { get; set; }
        public int CarVersionId { get; set; }
        public List<IFormFile> Files { get; set; }


    }
}
