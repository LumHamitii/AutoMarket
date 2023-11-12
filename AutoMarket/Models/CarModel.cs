using Microsoft.AspNetCore.Identity;

namespace AutoMarket.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        public string Mark { get; set; }

        public string Model { get; set; }

        public string Version { get; set; }

        public DateTime FirstRegistration { get; set; }

        public double Mileage { get; set; } 
        public string FuelType { get; set; }

        public int EnginePower { get; set; } 

        public string TransmissionType { get; set; }

        public float Price { get; set; }

        public string Condition { get; set; }

        public string Color { get; set; }

        public string InteriorColor { get; set; }

        public int NumberOfDoors { get; set; }

        public int NumberOfSeats { get; set; }

        public string Features { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
