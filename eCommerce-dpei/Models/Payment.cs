namespace eCommerce_dpei.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } 
        public string Status { get; set; } = "pending"; 
        public string? TransactionId { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public Order Order { get; set; }
        public Customer Customer { get; set; }
    }
}