using Microsoft.EntityFrameworkCore;
using TheShop.Order.Entities;

namespace TheShop.Order.Context
{
    public class OrderContext: DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerOrder>()
                .HasKey(x => x.CustomerOrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(x => x.OrderDetailId);

            modelBuilder.Entity<CustomerOrder>()
                .HasMany(x => x.OrderDetails)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.CustomerOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
