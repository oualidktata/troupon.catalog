
using Microsoft.EntityFrameworkCore;
using Troupon.Catalog.Core.Domain.Entities;
using System.Reflection;

namespace Troupon.Catalog.Infra.Persistence
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<DealEntity> Deals { get; set; }
        public DbSet<MerchantEntity> Merchants { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options):base(options)
        {
            //Database.EnsureCreated();
        }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
