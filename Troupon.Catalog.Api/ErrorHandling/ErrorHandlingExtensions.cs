using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Troupon.Catalog.Api.ErrorHandling
{
  public static class ErrorHandlingExtensions
  {
    public static void AddDomainExceptionHandlers(this IServiceCollection services, Assembly assembly)
    {
      var types = assembly.GetTypes().Where(t => t.IsAssignableFrom(typeof(IDomainExceptionHandler<IDomainException>)));
      foreach (var type in types)
      {
        services.AddSingleton(type);
      }
    }

    public static void UseErrorHandling(this IApplicationBuilder app)
    {
      app.UseExceptionHandler(errApp => errApp.Run(Handle));
    }

    public static async Task Handle(HttpContext context)
    {
      var error = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
      if (error == null)
      {
        return;
      }
    }
  }
}

  /*
    public static async Task Handle(HttpContext context)
    {
      var error = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
      if (error == null)
      {
        return;
      }

      /*if (error is DomainException)
      {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        await context.Response.WriteAsync(new ProblemDetails
        {
          Detail = error.Message,
          Status = (int)HttpStatusCode.BadRequest,
          Title = error.Message,
        }.ToString() ?? string.Empty);
      }
      else
      {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new ProblemDetails
        {
          Detail = "an error occured",
          Status = (int)HttpStatusCode.InternalServerError,
          Title = "an error occured",
        }.ToString() ?? string.Empty);
      }
    }
  }
}*/
