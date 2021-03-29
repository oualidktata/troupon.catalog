using Microsoft.Extensions.DependencyInjection;
using Troupon.Catalog.Infra.Persistence;
using Troupon.Catalog.Infra.Persistence.Repositories;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore;
using Troupon.Catalog.Service.Api.Schema;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Troupon.Catalog.Service.Api.DependencyInjectionExtensions
{
    public static class AddPersistanceExtensions
    {
        public static IServiceCollection AddPersistanceToApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("mainDatabaseConnStr");
            services.AddPooledDbContextFactory<CatalogDbContext>(
                 (serviceProvider, opt) =>
                 opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("Troupon.Catalog.Service.Api"))
                             .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>()));

            //services.AddScoped<IDealRepo,DealRepo>(provider=> new DealRepo(provider.GetRequiredService<IDbContextFactory<CatalogDbContext>>()));
            services.AddScoped<IMerchantRepo, MerchantRepo>(provider => new MerchantRepo(
                provider.GetRequiredService<IMapper>(),
                provider.GetRequiredService<IDbContextFactory<CatalogDbContext>>()));
            services.AddScoped<MerchantQueries>(provider => new MerchantQueries(provider.GetRequiredService<IMerchantRepo>()));
            //services.AddScoped<DealQueries>(provider => new DealQueries(provider.GetRequiredService<IDealRepo>()));

            //services.AddScoped<IApplicationRepo,ApplicationRepo>();
            services.AddScoped<IDealRepo, DealRepo>();
            return services;
        }
    }
}
