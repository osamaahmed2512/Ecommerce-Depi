using eCommerce_dpei.Data;
using eCommerce_dpei.repository;
using System.Collections;

namespace eCommerce_dpei.Models
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly EcommerceContext _context;
        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<Cart> Cart { get; private set; }
        public IGenericRepository<Order> Order { get; private set; }
        public IGenericRepository<OrderItem> OrderItem { get; private set; }
        public IGenericRepository<CustomerAddress> CustomerAddress { get; private set; }
        public IGenericRepository<Category> category { get; private set; }
        public UnitOfWork(EcommerceContext context)
        {
            _context = context;
            Products = new GenericRepository<Product>(_context);
            Cart = new GenericRepository<Cart>(_context);
            Order = new GenericRepository<Order>(_context);
            CustomerAddress = new GenericRepository<CustomerAddress>(_context);
            OrderItem = new GenericRepository<OrderItem>(_context);
            category = new GenericRepository<Category>(_context);
        }

        public int Complete()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
