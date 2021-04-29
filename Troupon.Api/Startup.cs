using Troupon.Catalog.Infra.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using Troupon.Catalog.Core.Application;
using Troupon.Catalog.Service.Api.DependencyInjectionExtensions;
using Serilog;
using HealthChecks.UI.Client;

namespace Troupon.Catalog.Service.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var value = Configuration.GetValue<string>("Global:tenant");
            

            services.AddAuthenticationToApplication(Configuration,HostEnvironment);
            services.AddAuthorization(options => {
               // options.AddPolicy("crm-api-backend", policy => policy.RequireClaim("crm-api-backend", "[crm-api-backend]"));
            });


            services.AddAutoMapper(typeof(AutomapperProfile), typeof(AutomapperProfileDomain));
            services.AddMediatorToApplication();
            services.AddPersistanceToApplication(Configuration);
            services.AddGraphQLToApplication();//https://localhost:5001/graphql/
            services.AddControllers();
            services.AddOpenApiToApplication(Configuration);
            services.AddHealthChecksToApplication(Configuration);
            services.AddHealthChecksUIToApplication();
             services.AddMetrics();
            services.AddFluentValidatonToApplication();
            services.AddMemoryCache();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            app.UseExceptionHandler("/error");
           
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
           // app.UsePathBase("/graphql");

            //using (CatalogDbContext)
            //{
            //    CatalogDbContext.Database.EnsureDeleted();
            //    CatalogDbContext.Database.Migrate();
            //}
             //app.UsePlayground();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Troupon Catalog");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("all"),ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse });
                endpoints.MapHealthChecks("/health/external", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("external"), ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
                endpoints.MapHealthChecks("/health/db", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("db"), ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
                endpoints.MapHealthChecks("/health/uri", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("uri"), ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
                endpoints.MapHealthChecks("/health/internal", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("errors")|| check.Tags.Contains("db"), ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
                //endpoints.MapHealthChecks("/health/scheduler", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("scheduler"), ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
                endpoints.MapHealthChecksUI();
                endpoints.MapGraphQL();
            });
        }

        private async Task JsonHealthReport(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, new { Status=report.Status.ToString()},
                new JsonSerializerOptions { PropertyNamingPolicy=JsonNamingPolicy.CamelCase});
        }
    }
}
