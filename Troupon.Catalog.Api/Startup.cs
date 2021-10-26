using System;
using System.Reflection;
using Infra.Authorization.Policies;
using Infra.MediatR;
using Infra.OAuth;
using Infra.OAuth.Controllers;
using Infra.OAuth.Introspection;
using Infra.Persistence.Dapper.Extensions;
using Infra.Persistence.EntityFramework.Extensions;
using Infra.Persistence.SqlServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Troupon.Catalog.Api.DependencyInjectionExtensions;
using Troupon.Catalog.Api.FluentValidatonToMove;
using Troupon.Catalog.Core.Application.Queries.Deals;
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

      services.AddSingleton<IOAuthSettingsFactory>(sp => new OAuthSettingsFactory(Configuration));
      services.AddScoped<IM2MOAuthFlowService, M2MOAuthFlowService>();
      services.AddOAuthGenericAuthentication();
      // TODO: Ajouter le bon assembly ici
      services.AddDomainExceptionHandlers(TelHandler.Assembly);
      services.AddScoped<IAuthorizationHandler, RequireTenantClaimHandler>();
      services.AddAutoMapper(
        typeof(AutomapperProfile));

      services.AddAuthorization(options =>
      {
        options.AddPolicy(TenantPolicy.Key, pb => pb.AddTenantPolicy("pwc"));
        options.AddPolicy(AdminOnlyPolicy.Key, pb => pb.AddAdminOnlyPolicy());
      });

      services.AddPolicyHandlers();

      services.AddAutoMapper(typeof(AutomapperProfile));

      services.AddMediator(typeof(GetDealsQuery).Assembly);
      services.AddSqlServerPersistence<CatalogDbContext>(
        Configuration,
        "mainDatabaseConnStr",
        Assembly.GetExecutingAssembly().GetName().Name);

      services.AddControllers()
       .AddNewtonsoftJson();

      services.AddControllers()
       .AddApplicationPart(typeof(OAuthController).Assembly)
       .AddControllersAsServices();

      services.AddEfReadRepository<CatalogDbContext>();
      services.AddEfWriteRepository<CatalogDbContext>();
      services.AddOpenApi(Assembly.GetExecutingAssembly());
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
      app.UseErrorHandling();

      app.UseHttpsRedirection();
      app.UseSerilogRequestLogging();

      var catalogDbContext = dbContextFactory.CreateDbContext();
      catalogDbContext.Database.Migrate();

      app.UseSwagger();

      var factory = serviceProvider.GetRequiredService<IOAuthSettingsFactory>();
      app.ConfigureSwaggerUI(apiVersionDescriptionProvider, factory.GetDefaultMachineToMachine());

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(
        endpoints =>
        {
          endpoints.MapControllers();
        });
    }
  }
}
