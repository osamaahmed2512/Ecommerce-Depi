using Ecommerce.domain.common;

namespace Ecommerce.domain.entities
{
    public class Order:BaseEntity
    {
        public string UserId { get; set; }
        public int AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "pending";
        public string? ShippingTrackingNumber { get; set; }
        public Address Address { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
