using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Troupon.Catalog.Core.Domain.Entities.Deal;

namespace Troupon.Catalog.Infra.Persistence
{
  public class CatalogDbContext : DbContext
  {
    public DbSet<Deal> Deals { get; set; }

    public CatalogDbContext(
      DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(
      ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("Troupon.Catalog");
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
  }
}
