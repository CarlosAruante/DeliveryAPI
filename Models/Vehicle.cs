
namespace DeliveryAPI.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string  Plate { get; set; }
        public float CurrentLat { get; set; }
        public float CurrentLng { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }
    }
}
