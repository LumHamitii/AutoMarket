using System.Text.Json.Serialization;

namespace AutoMarket.Models
{
    public class TruckPhoto
    {
        public int Id { get; set; }
        public byte[] PhotoData { get; set; }
        public string ContentType { get; set; }
        public int TruckId { get; set; }
        [JsonIgnore]
        public Truck Truck { get; set; }
    }
}
