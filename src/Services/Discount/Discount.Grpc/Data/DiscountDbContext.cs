using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountDbContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountDbContext(DbContextOptions<DiscountDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>()
                .HasData(
                    new Coupon { Id = 1, ProductName = "New Product Superior", Description = "The greatest product we have", Amount = 150 },
                    new Coupon { Id = 2, ProductName = "Canon EOS R5", Description = "This mirrorless camera delivers stunning 45MP resolution with 8K video capabilities and advanced autofocus for professional photography.", Amount = 3000 }
                );
        }
    }
}