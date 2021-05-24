using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Troupon.Checkout.Core.Domain.Entities.Common;
using Troupon.Checkout.Core.Domain.Entities.Customer;
using Troupon.Checkout.Core.Domain.Entities.Order;

namespace Troupon.Checkout.Infra.Persistence
{
  public class CheckoutDbContext : DbContext
  {
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public CheckoutDbContext(
      DbContextOptions<CheckoutDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(
      ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("Troupon.Checkout");
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
  }
}
