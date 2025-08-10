using Ecommerce.domain.common;

namespace Ecommerce.domain.entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductImage> ProductImage { get; set; }
    }
}
