
namespace DeliveryAPI.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public int IdVehicle { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime DateDelivery { get; set; }

        public  ICollection<Order> Orders { get; set; }
    }
}
