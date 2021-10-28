using Infra.Exceptions.ExceptionHandling;
using Microsoft.AspNetCore.Mvc;

namespace Troupon.Catalog.Core.Domain.Exceptions
{
    public class POCDomainExceptionHandler : IDomainExceptionHandler<POCDomainException>
    {
      public ProblemDetails Handle(POCDomainException exception, bool showDetails)
      {
        return new ProblemDetails
        {
          Title = exception.Message,
          Detail = showDetails? exception.StackTrace : string.Empty,
          Status = 400,
        };
      }
    }
}
