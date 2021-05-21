using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Troupon.Catalog.Core.Domain.Entities.Account;
using Troupon.Catalog.Core.Domain.Entities.Category;
using Troupon.Catalog.Core.Domain.Entities.Common;
using Troupon.Catalog.Core.Domain.Entities.Customer;
using Troupon.Catalog.Core.Domain.Entities.Deal;
using Troupon.Catalog.Core.Domain.Entities.Merchant;
using Troupon.Catalog.Core.Domain.Entities.Order;

namespace Troupon.Catalog.Infra.Persistence
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealOption> DealOptions { get; set; }
        public DbSet<DealPrice> DealPrices { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<DealCategory> DealCategories { get; set; }
        public DbSet<BillingInfo> BillingInfos { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public CatalogDbContext(
            DbContextOptions<CatalogDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Troupon");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
