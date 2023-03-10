namespace DeliveryTracking.Models
{
    public class Order
    {
        private static string GetRandomID()
        {
            Random generator = new Random();
            string r = generator.Next(100000000, 999999999).ToString();
            return r;
        }

        public int Id { get; set; }
        public string OrderId { get; set; } = "OD" + GetRandomID(); 
        public string DeliveryDate { get; set; }    
        public string ShippedBy { get; set; }   
        public string Status { get; set; }
        public string TrackingId { get; set; } = "CR" + GetRandomID();
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
