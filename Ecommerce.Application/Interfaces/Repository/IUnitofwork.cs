using Ecommerce.domain.entities;

namespace Ecommerce.Application.Interfaces.Repository
{
    public interface IUnitofwork:IDisposable
    {
        IGenericRepository<TokenInfo> TokenRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<ProductImage> ProductImageRepository { get; }
        IGenericRepository<Cart> CartRepository { get; }
        IProductImageRepository ProductImageRepo { get; }
        IGenericRepository<Address> AddressRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        Task<int> CompleteAsync();
    }
}
