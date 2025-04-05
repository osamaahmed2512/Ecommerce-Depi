using eCommerce_dpei.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_dpei.Data
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}