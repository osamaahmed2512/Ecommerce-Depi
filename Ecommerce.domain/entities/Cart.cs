using Ecommerce.domain.common;
namespace Ecommerce.domain.entities
{
    public class Cart:BaseEntity
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
