using System.Reflection;
using Infra.Api.DependencyInjection;
using Infra.Authorization.Policies;
using Infra.ExceptionHandling.Controllers;
using Infra.ExceptionHandling.Extensions;
using Infra.MediatR;
using Infra.OAuth.Controllers.DependencyInjection;
using Infra.OAuth.DependencyInjection;
using Infra.Persistence.Dapper.Extensions;
using Infra.Persistence.EntityFramework.Extensions;
using Infra.Persistence.SqlServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Troupon.Catalog.Api.DependencyInjectionExtensions;
using Troupon.Catalog.Api.ToMoveOrRemove;
using Troupon.Catalog.Core.Application.Queries.Deals;
using Troupon.Catalog.Core.Domain.Exceptions;
using Troupon.Catalog.Infra.Persistence;

namespace Troupon.Catalog.Api
{
  public class Startup
  {
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddOAuthGenericAuthentication(Configuration)
          .AddOAuthM2MAuthFlow();

      services.AddControllers().AddNewtonsoftJson();
      services.AddOAuthController();

      services.AddAutoMapper(
        typeof(AutomapperProfile));

      services.AddAuthorization(options =>
      {
        options.AddPolicy(TenantPolicy.Key, pb => pb.AddTenantPolicy("pwc"));
        options.AddPolicy(AdminOnlyPolicy.Key, pb => pb.AddAdminOnlyPolicy());
      });

      services.AddPolicyHandlers();

      services.AddAutoMapper(typeof(AutomapperProfile).Assembly);

      services.AddMediator(typeof(GetDealsQuery).Assembly);
      services.AddSqlServerPersistence<CatalogDbContext>(
        Configuration,
        "mainDatabaseConnStr",
        Assembly.GetExecutingAssembly().GetName().Name);

      // exception handling
      services.AddWebExceptionHandler();
      services.AddDomainExceptionHandlers(typeof(DealDoesntExistExceptionHandler).Assembly);
      services.AddControllers()
       .AddApplicationPart(typeof(ErrorController).Assembly)
       .AddControllersAsServices();
      services.AddEfReadRepository<CatalogDbContext>();
      services.AddEfWriteRepository<CatalogDbContext>();
      services.AddOpenApi(Assembly.GetExecutingAssembly());
      services.AddMetrics();
      services.AddFluentValidaton();
      services.AddMemoryCache();
      services.AddDapperPersistence("mainDatabaseConnStr");

      services.Configure<MvcOptions>(opt =>
      {
        opt.Filters.Add(new ProducesAttribute("application/json", "application/xml"));
        opt.Filters.Add(new ConsumesAttribute("application/json", "application/xml"));
      });

      services.AddPwcApiBehaviour();
    }

    public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider, IDbContextFactory<CatalogDbContext> dbContextFactory)
    {
      // exception handling (wrapper for .net userExceptionHandler)
      app.UseErrorHandling();

      app.UseHttpsRedirection();
      app.UseSerilogRequestLogging();

      var catalogDbContext = dbContextFactory.CreateDbContext();
      catalogDbContext.Database.Migrate();

      app.UseSwagger();

      app.ConfigureSwaggerUI(apiVersionDescriptionProvider);

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
