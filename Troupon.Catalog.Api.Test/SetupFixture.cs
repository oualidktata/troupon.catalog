using System.IO;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Troupon.Catalog.Api;
using Troupon.Catalog.Core.Domain.InputModels;
using Troupon.Catalog.Core.Domain.Dtos;
using FluentAssertions;
using FluentResults.Extensions.FluentAssertions;
using FluentResults;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Troupon.Catalog.Infra.Persistence;
using Respawn;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class SetupFixture : IClassFixture<WebApplicationFactory<Startup>>
{
  protected string BaseUri => "https://localhost:5001/api/catalog";
  protected readonly WebApplicationFactory<Startup> Factory;
  protected static IServiceScopeFactory _scopeFactory;
  protected static Checkpoint Checkpoint;
  private static IConfiguration _configuration;
  public SetupFixture(WebApplicationFactory<Startup> factory)
  {
    Factory = factory;
    var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddEnvironmentVariables();

    _configuration = builder.Build();
    _scopeFactory = Factory.Services.GetService<IServiceScopeFactory>();
    Checkpoint = new Checkpoint()
    {
      TablesToIgnore = new[] { "__EFMigrationsHistory" }
    };
    //Replace repo by in-memory repo
    var efRepoDescriptor = Factory.Services.GetServices(typeof(CatalogDbContext));
    Factory.WithWebHostBuilder(builder=>builder.ConfigureServices(services=> {
      var dbContextDescriptor=services.FirstOrDefault(d => d.ServiceType == typeof(CatalogDbContext));
      services.Remove(dbContextDescriptor);
      services.AddPooledDbContextFactory<CatalogDbContext>(
                (
                        serviceProvider,
                        opt) =>
                    opt
                        .UseLazyLoadingProxies()
                        .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

    }));

  }

  public static async Task ResetState()
  {
    await Checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
  }
}
