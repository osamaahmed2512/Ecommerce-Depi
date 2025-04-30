using eCommerce_dpei.Models;

namespace eCommerce_dpei.DTOS
{
    public class PaginatedProductsDto
    {
        public int TotalCount { get; set; }
        public List<Product>? Products { get; set; }
    }
}
