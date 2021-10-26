using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Infra.Exceptions.ExceptionHandling.Extensions;
using Infra.oAuthService;
using Infra.Persistence.Dapper.Extensions;
using Infra.Persistence.EntityFramework.Extensions;
using Infra.Persistence.SqlServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Troupon.Catalog.Api.DependencyInjectionExtensions;
using Troupon.Catalog.Core.Application;
using Troupon.Catalog.Infra.Persistence;

namespace Troupon.Catalog.Api
{
  public class Startup
  {
    public Startup(
      IConfiguration configuration,
      IWebHostEnvironment hostEnvironment)
    {
      Configuration = configuration;
      HostEnvironment = hostEnvironment;
      AuthSettings = new OAuthSettings();
      Configuration.GetSection($"Auth:{Configuration.GetValue<string>("Auth:DefaultAuthProvider")}")
        .Bind(AuthSettings);
    }

    private IOAuthSettings AuthSettings { get; }

    private IConfiguration Configuration { get; }

    private IWebHostEnvironment HostEnvironment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(
      IServiceCollection services)
    {
      services.AddScoped<IAuthService>(service => new AuthService(AuthSettings));
      IAuthService authService = services.BuildServiceProvider().GetRequiredService<IAuthService>(); // TODO: Try another way to avoid BuildServiceProvider(not a priority)...
      services.AddAuthenticationToApplication(authService, Configuration, HostEnvironment);
      services.AddAuthorization(
        options =>
        {
          options.AddPolicy("tenant-policy", pb => pb.AddTenantPolicy("pwc"));
        });

      // TODO: Ajouter le bon assembly ici
      services.AddDomainExceptionHandlers(TelHandler.Assembly);
      services.AddScoped<IAuthorizationHandler, RequireTenantClaimHandler>();
      services.AddAutoMapper(
        typeof(AutomapperProfile));

      services.AddMediator();
      services.AddSqlServerPersistence<CatalogDbContext>(
        Configuration,
        "mainDatabaseConnStr",
        Assembly.GetExecutingAssembly().GetName().Name);
      services.AddControllers()
        .AddNewtonsoftJson();
      services.AddEfReadRepository<CatalogDbContext>();
      services.AddEfWriteRepository<CatalogDbContext>();
      services.AddOpenApi(AuthSettings);
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
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider, IDbContextFactory<CatalogDbContext> dbContextFactory)
    {
      app.UseErrorHandling();

      app.UseHttpsRedirection();
      app.UseSerilogRequestLogging();

      var catalogDbContext = dbContextFactory.CreateDbContext();
      catalogDbContext.Database.Migrate();

      app.UseSwagger();
      app.ConfigureSwaggerUI(apiVersionDescriptionProvider, AuthSettings);
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(
        endpoints => { endpoints.MapControllers(); });
    }
  }
}
