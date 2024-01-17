using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMarket.Models
{
    public class Motorcycle
    {
        public int Id { get; set; }

        [DisplayName("Registration Date")]
        public DateTime FirstRegistration { get; set; }

        [DisplayName("Engine Power")]
        public int EnginePower { get; set; }

        public float Price { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }
        public int MotorcycleBrandId { get; set; }

      
        public int MotorcycleModelId { get; set; }


        public int MotorcycleTypeId { get; set; }

        public int MotorcycleColorId { get; set; }

        public int MotorcycleMileageId { get; set; }

        public int MotorcycleConditionId { get; set; }

        public int MotorcycleTransmissionId { get; set; }

        public int MotorcycleFuelTypeId { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }


        public MotorcycleBrand MotorcycleBrand { get; set; }

        public MotorcycleColor MotorcycleColor { get; set; }
        public MotorcycleCondition MotorcycleCondition { get; set; }
        public MotorcycleFuelType MotorcycleFuelType { get; set; }
        public MotorcycleMileage MotorcycleMileage { get; set; }
        public MotorcycleModel MotorcycleModel { get; set; }
        public MotorcycleTransmission MotorcycleTransmission { get; set; }
        public MotorcycleType MotorcycleType { get; set; }
        public List<MotorcyclePhoto> MotorcyclePhotos { get; set; }

    }
}
