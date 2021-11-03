using Infra.ExceptionHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Troupon.Catalog.Core.Domain.Exceptions
{
    public class DealDoesntExistExceptionHandler : DomainExceptionHandler<DealDoesntExistException>
    {
      public override ProblemDetails HandleException(DealDoesntExistException exception, bool showDetails)
      {
        return new ProblemDetails
        {
          Title = exception.Message,
          Detail = showDetails? exception.StackTrace : string.Empty,
          Status = StatusCodes.Status400BadRequest,
        };
      }
    }
}
