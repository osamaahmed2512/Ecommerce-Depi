using eCommerce_dpei.Models;

namespace eCommerce_dpei.repository
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Cart> Cart { get;  }
        IGenericRepository<Order> Order { get;  }
        IGenericRepository<CustomerAddress> CustomerAddress { get;  }
        public IGenericRepository<OrderItem> OrderItem { get; }
        public IGenericRepository<Category> category { get;  }
        int Complete();
    }
}
