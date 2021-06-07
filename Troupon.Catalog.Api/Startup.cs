using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Infra.oAuthService;
using Infra.Persistence.EntityFramework.Extensions;
using Infra.Persistence.SqlServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    }

    private IConfiguration Configuration { get; }
    private IWebHostEnvironment HostEnvironment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(
      IServiceCollection services)
    {
      var apiKeySettings = new OAuthSettings();
      Configuration.GetSection($"Auth:{Configuration.GetValue<string>("Auth:DefaultAuthProvider")}")
        .Bind(apiKeySettings);
      services.AddScoped<IAuthService>(service => new AuthService(apiKeySettings));

      // services.AddAuthenticationToApplication(
      //   new AuthService(apiKeySettings),
      //   Configuration,
      //   HostEnvironment);
      services.AddAuthorization(
        options =>
        {
          //options.AddPolicy("crm-api-backend", policy => policy.RequireClaim("crm-api-backend", "[crm-api-backend]"));
        });

      services.AddAutoMapper(
        typeof(AutomapperProfile));

      services.AddMediator();
      services.AddSqlServerPersistence<CatalogDbContext>(
        Configuration,
        "mainDatabaseConnStr",
        Assembly.GetExecutingAssembly()
          .GetName()
          .Name);
      services.AddEfReadRepository<CatalogDbContext>();
      services.AddEfWriteRepository<CatalogDbContext>();
      services.AddControllers();
      services.AddOpenApi(Configuration);
      services.AddMetrics();
      services.AddFluentValidaton();
      services.AddMemoryCache();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
      IApplicationBuilder app,
      IWebHostEnvironment env,
      IDbContextFactory<CatalogDbContext> dbContextFactory)
    {
      //if (env.IsDevelopment())
      //{
      //    app.UseDeveloperExceptionPage();
      //}
      app.UseExceptionHandler("/error");

      app.UseHttpsRedirection();
      app.UseSerilogRequestLogging();

      // app.UsePathBase("/graphql");

      //catalogDbContext.Database.EnsureDeleted();
      var catalogDbContext = dbContextFactory.CreateDbContext();
      catalogDbContext.Database.Migrate();

      // app.UsePlayground();

      app.UseSwagger();
      app.UseSwaggerUI(
        c =>
        {
          c.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Troupon Catalog");
          c.RoutePrefix = string.Empty;
        });
      app.UseRouting();
      //app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(
        endpoints =>
        {
          endpoints.MapControllers();
        });
    }
  }
}
