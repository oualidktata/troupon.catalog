using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Troupon.Catalog.Api.ErrorHandling
{
  public abstract class DomainException : Exception
  {
  }

  public interface IDomainExceptionHandler
  {
    ProblemDetails Handle(DomainException exception);
  }

  public interface IDomainExceptionHandler<T> : IDomainExceptionHandler
    where T : DomainException
  {
  }
}
