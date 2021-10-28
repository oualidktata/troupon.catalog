using System.Linq;
using System.Reflection;
using Infra.Exceptions.ExceptionHandling.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Exceptions.ExceptionHandling.Extensions
{
  public static class ErrorHandlingExtensions
  {
    public static void AddDomainExceptionHandlers(this IServiceCollection services, Assembly assembly)
    {
      var types = assembly.GetTypes().Where(t => t.IsAssignableFrom(typeof(IDomainExceptionHandler<DomainException>)));
      foreach (var type in types)
      {
        services.AddSingleton(type);
      }
    }

    public static void AddExceptionHandling(this IServiceCollection services)
    {
      services.AddSingleton<IGenericExceptionHandler, GenericWebExceptionHandler>();
    }

    public static void UseErrorHandling(this IApplicationBuilder app)
    {
      app.UseExceptionHandler("/Error");
    }
  }
}
