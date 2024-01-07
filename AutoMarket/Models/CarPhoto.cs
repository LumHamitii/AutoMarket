using System.Text.Json.Serialization;

namespace AutoMarket.Models
{
    public class CarPhoto
    {
        public int Id { get; set; }
        public byte[] PhotoData { get; set; }  
        public string ContentType { get; set; } 
        public int CarId { get; set; }
        [JsonIgnore]
        public Car Car { get; set; }
    }
}
