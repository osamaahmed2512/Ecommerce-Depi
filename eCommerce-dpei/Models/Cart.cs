namespace eCommerce_dpei.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "pending";
        public string? ShippingTrackingNumber { get; set; }
        public Customer Customer { get; set; }
        public CustomerAddress Address { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Payment? Payment { get; set; }
    }
}