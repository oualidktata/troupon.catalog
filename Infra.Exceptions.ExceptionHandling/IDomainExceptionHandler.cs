using Microsoft.AspNetCore.Mvc;

namespace Infra.Exceptions.ExceptionHandling
{
  public interface IDomainExceptionHandler: IDomainExceptionHandler<DomainException>
  {
  }

  public interface IDomainExceptionHandler<T>
    where T : DomainException
  {
    ProblemDetails Handle(T exception);
  }
}
