using System.Text.Json.Serialization;


namespace AutoMarket.Models
{

    public class MotorcyclePhoto
    {
        public int Id { get; set; }
        public byte[] PhotoData { get; set; }
        public string ContentType { get; set; }
        public int MotorcycleId { get; set; }
        [JsonIgnore]
        public Motorcycle Motorcycle { get; set; }
    }
}
