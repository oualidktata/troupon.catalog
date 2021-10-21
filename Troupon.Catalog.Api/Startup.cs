using System;
using System.Reflection;
using Infra.oAuthService;
using Infra.Persistence.Dapper.Extensions;
using Infra.Persistence.EntityFramework.Extensions;
using Infra.Persistence.SqlServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Troupon.Catalog.Api.AuthIntrospection;
using Troupon.Catalog.Api.Authorization;
using Troupon.Catalog.Api.Authorization.RequirementHandlers;
using Troupon.Catalog.Api.DependencyInjectionExtensions;
using Troupon.Catalog.Infra.Persistence;

namespace Troupon.Catalog.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHttpContextAccessor();
      services.AddScoped<IJwtIntrospector, JwtIntrospector>();

      services.AddSingleton<IOAuthSettingsFactory>(sp => new OAuthSettingsFactory(Configuration, "Auth"));
      services.AddScoped<IMachineToMachineOAuthService, MachineToMachineOAuthService>();
      services.AddAuthenticationToApplication();

      services.AddAuthorization(options =>
       {
         // var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder("Bearer");
         // defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
         // options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
         options.AddPolicy("tenant-policy", pb => pb.AddTenantPolicy("pwc"));

         // options.AddPolicy("admin-policy", p => p.AddAdminPolicy());
       });

      // services.AddScoped<RequireTenantClaimHandler>();
      // services.AddScoped<RequireAdminClaimHandler>();
      services.AddAutoMapper(typeof(AutomapperProfile));

      services.AddMediator();
      services.AddSqlServerPersistence<CatalogDbContext>(
        Configuration,
        "mainDatabaseConnStr",
        Assembly.GetExecutingAssembly().GetName().Name);

      services.AddControllers()
        .AddNewtonsoftJson();

      services.AddEfReadRepository<CatalogDbContext>();
      services.AddEfWriteRepository<CatalogDbContext>();
      services.AddOpenApi();
      services.AddMetrics();
      services.AddFluentValidaton();
      services.AddMemoryCache();
      services.AddDapperPersistence("mainDatabaseConnStr");

      services.Configure<MvcOptions>(o =>
      {
        o.Filters.Add(new ProducesAttribute("application/json", "application/xml"));
        o.Filters.Add(new ConsumesAttribute("application/json", "application/xml"));
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IApiVersionDescriptionProvider apiVersionDescriptionProvider, IDbContextFactory<CatalogDbContext> dbContextFactory)
    {
      app.UseExceptionHandler("/error");
      app.UseHttpsRedirection();
      app.UseSerilogRequestLogging();

      // app.UsePathBase("/graphql");

      // catalogDbContext.Database.EnsureDeleted();
      var catalogDbContext = dbContextFactory.CreateDbContext();
      catalogDbContext.Database.Migrate();

      // app.UsePlayground();
      app.UseSwagger();

      var factory = serviceProvider.GetRequiredService<IOAuthSettingsFactory>();
      app.ConfigureSwaggerUI(apiVersionDescriptionProvider, factory.GetDefaultMachineToMachine());

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(
        endpoints => { endpoints.MapControllers(); });
    }
  }
}
