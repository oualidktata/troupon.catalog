using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Api.DependencyInjectionExtensions
{
  public static class AddFluentValidatonExtensions
  {
    public static IServiceCollection AddFluentValidaton(
      this IServiceCollection services)
    {
      services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

      return services;
    }
  }
}
