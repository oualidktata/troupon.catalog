using Infra.ExceptionHandling;

namespace Troupon.Catalog.Core.Domain.Exceptions
{
  public class DealDoesntExistException : DomainException
  {
    public DealDoesntExistException(string? message)
      :base(message)
    {
    }
  }
}
