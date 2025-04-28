namespace eCommerce_dpei.DTOS
{
    public class CheckoutResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int OrderId { get; set; }
        public decimal Total { get; set; }
    }
}
