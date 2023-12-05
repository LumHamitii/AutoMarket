using Microsoft.AspNetCore.Identity;


namespace AutoMarket.Models
{
    public class Car
    {
        public int Id { get; set; }

        public DateTime FirstRegistration { get; set; }

        public int EnginePower { get; set; } 

        public float Price { get; set; }


        public string Features { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
        public int CarBrandId { get; set; }
        public int CarModelId { get; set; }
        public int CarFuelTypeId { get; set; }
        public int CarColorId { get; set; }
        public int CarConditionId { get; set; }
        public int CarMileageId { get; set; }
        public int CarSeatsId { get; set; }
        public int CarTransmissionTypeId { get; set; }
        public int CarVersionId { get; set; }

       

        public IdentityUser User { get; set; }

        public CarBrand CarBrand { get; set; }
        public CarModel CarModel { get; set; }
        public CarCondition CarCondition { get; set; }
        public CarColor CarColor { get; set; }
        public CarFuelType CarFuelType { get; set; }  
        public CarMileage CarMileage { get; set; }
        public CarSeats CarSeats { get; set; }
        public CarTransmissionType CarTransmissionType { get; set; }
        public CarVersion CarVersion { get; set; }  

    }
}
