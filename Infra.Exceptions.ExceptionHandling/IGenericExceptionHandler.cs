using System;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Exceptions.ExceptionHandling
{
  public interface IGenericExceptionHandler
  {
    ProblemDetails Handle(Exception? exception);
  }
}
