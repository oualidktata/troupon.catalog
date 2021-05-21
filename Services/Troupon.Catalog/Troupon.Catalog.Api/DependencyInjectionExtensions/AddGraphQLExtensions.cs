using Microsoft.Extensions.DependencyInjection;
using Troupon.Catalog.Api.Schema;

namespace Troupon.Catalog.Api.DependencyInjectionExtensions
{
  public static class AddGraphQlExtensions
  {
    public static IServiceCollection AddGraphQl(
      this IServiceCollection services)
    {
      //services.AddDataLoaderRegistry();
      services.AddGraphQLServer()

        //.AddQueryType(desc => desc.Name("Query"))
        //.AddType<DealQueries>()
        .AddQueryType<MerchantQueries>()
        .AddFiltering()
        .AddProjections()
        .AddSorting();

      //.AddType<DealResolvers>()
      //.ModifyOptions(opts => opts.RemoveUnreachableTypes = true);
      //.AddApolloTracing();
      return services;
    }
  }
}
