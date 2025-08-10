using Ecommerce.domain.entities;
using Ecommerce.infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.infrastructure.percistence
{
    public class ApplcationDbContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplcationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .Property(x => x.Price)
                .HasPrecision(10, 2);
           builder.Entity<Order>()
                .Property(o =>o.TotalPrice)
                .HasPrecision(18, 2);
           builder.Entity<OrderItem>()
                .Property(o=>o.UnitPrice)
                .HasPrecision(18, 2);

            base.OnModelCreating(builder);
        }
        public DbSet<TokenInfo> TokenInfos { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Address { get; set; } 
    }
}
