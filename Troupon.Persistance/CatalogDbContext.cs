using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Troupon.Catalog.Core.Domain.Entities.Account;
using Troupon.Catalog.Core.Domain.Entities.Category;
using Troupon.Catalog.Core.Domain.Entities.Deal;
using Troupon.Catalog.Core.Domain.Entities.Merchant;

namespace Troupon.Catalog.Infra.Persistence
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<DealCategory> DealCategories { get; set; }

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
