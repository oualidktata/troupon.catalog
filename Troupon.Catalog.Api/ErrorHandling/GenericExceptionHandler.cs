using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Troupon.Catalog.Api.ErrorHandling
{
  public class GenericExceptionHandler : IGenericExceptionHandler
  {
    private readonly IServiceProvider serviceProvider;

    public GenericExceptionHandler(IServiceProvider serviceProvider)
    {
      this.serviceProvider = serviceProvider;
    }

    public ProblemDetails Handle(HttpContext httpContext)
    {
      var context = httpContext.Features.Get<IExceptionHandlerPathFeature>();
      var error = context?.Error;

      // error should never be null but we have to check anyway
      if (error != null && error is DomainException)
      {
        return HandleDomainExceptions((DomainException)error);
      }
      else
      {
        return HandleOtherExceptions(error);
      }
    }

    public ProblemDetails HandleDomainExceptions(DomainException exception)
    {
      var genericHandler = typeof(IDomainExceptionHandler<>);
      genericHandler.MakeGenericType(new Type[] { exception.GetType() });

      var specificHandler = serviceProvider.GetService(genericHandler) as IDomainExceptionHandler;
      if (specificHandler == null)
      {
        return HandleOtherExceptions(exception);
      }

      return specificHandler.Handle(exception);
    }

    public ProblemDetails HandleOtherExceptions(Exception exception)
    {
      return new ProblemDetails();
    }
  }

  public interface IGenericExceptionHandler
  {
    ProblemDetails Handle(HttpContext httpContext);
  }
}
