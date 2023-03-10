namespace DeliveryTracking.Dtos
{
    public class AddOrderDto
    {
        public string? DeliveryDate { get; set; }
        public string? ShippedBy { get; set; }
        public string? Status { get; set; }
        public ICollection<AddItemDto> Items { get; set; } = new List<AddItemDto>();
    }
}
