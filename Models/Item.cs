using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DeliveryTracking.Models
{
    public class Item
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public decimal Price { get; set; }
        [JsonIgnore, IgnoreDataMember] 
        public Order Order { get; set; }
        public int OrderId { get; set; }    
    }
}
