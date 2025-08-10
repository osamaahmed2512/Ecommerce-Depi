using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.domain.entities;


namespace Ecommerce.infrastructure.percistence.Repository
{
    public class Unitofwork : IUnitofwork
    {
        private readonly ApplcationDbContext _context;

        public IGenericRepository<TokenInfo> TokenRepository { get; }
        public IGenericRepository<Category> CategoryRepository { get; }
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<ProductImage> ProductImageRepository { get; }
        public IGenericRepository<Cart> CartRepository { get; }
        public IProductImageRepository ProductImageRepo { get; }
        public IGenericRepository<Address> AddressRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public Unitofwork(ApplcationDbContext context)
        {
            _context = context;
            TokenRepository = new Repository<TokenInfo>(_context);
            CategoryRepository = new Repository<Category>(_context);
            ProductRepository = new Repository<Product>(_context);
            ProductImageRepository = new Repository<ProductImage>(_context);
            ProductImageRepo= new ProductImageRepository(_context);
            CartRepository = new Repository<Cart>(_context);
            AddressRepository = new Repository<Address>(_context);
            OrderRepository = new Repository<Order>(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
