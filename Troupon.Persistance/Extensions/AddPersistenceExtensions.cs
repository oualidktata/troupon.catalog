using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Troupon.Catalog.Infra.Persistence.Extensions
{
    public static class AddPersistenceExtensions
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName,
            string runningAssembly)
        {
            var connectionString = configuration.GetConnectionString(connectionStringName);
            services.AddPooledDbContextFactory<CatalogDbContext>(
                (
                        serviceProvider,
                        opt) =>
                    opt
                        .UseLazyLoadingProxies()
                        .UseSqlServer(
                            connectionString,
                            b => b.MigrationsAssembly(runningAssembly))
                        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>()));

            return services;
        }
    }
}
