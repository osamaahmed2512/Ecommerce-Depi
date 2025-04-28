namespace eCommerce_dpei.DTOS
{
    public class CartSummaryDto
    {
        public List<CartItemSummaryDto> Items { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
