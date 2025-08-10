using Ecommerce.domain.common;

namespace Ecommerce.domain.entities
{
    public class ProductImage:BaseEntity
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; } = false;
        public Product Product { get; set; }
    }
}
