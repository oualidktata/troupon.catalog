using System;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Exceptions.ExceptionHandling
{
  public class GenericExceptionHandler : IGenericExceptionHandler
  {
    private readonly IServiceProvider serviceProvider;

    public GenericExceptionHandler(IServiceProvider serviceProvider)
    {
      this.serviceProvider = serviceProvider;
    }

    ProblemDetails IGenericExceptionHandler.Handle(Exception? exception)
    {
      throw new NotImplementedException();
    }

    public ProblemDetails Handle(Exception? exception)
    {
      // error should never be null but we have to check anyway
      if (exception != null && exception is DomainException)
      {
        return HandleDomainExceptions((DomainException)exception);
      }
      else
      {
        return HandleOtherExceptions(exception);
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

    public static ProblemDetails HandleOtherExceptions(Exception? exception)
    {
      return new ProblemDetails();
    }
  }
}
