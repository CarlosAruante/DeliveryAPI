namespace DeliveryAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public string Address { get; set; }
        public float Lat {  get; set; }
        public float Lng { get; set; }

        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
    }
}
