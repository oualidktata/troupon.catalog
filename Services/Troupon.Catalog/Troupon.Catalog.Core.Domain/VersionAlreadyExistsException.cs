using System;
using ApplicationException = Infra.Common.Models.Exceptions.ApplicationException;

namespace Troupon.Catalog.Core.Domain
{
  public class DealAlreadyExistsException : ApplicationException
  {
    public DealAlreadyExistsException(
      string message) : base(message)
    {
    }
  }

  public class CRMIntergrationException : ApplicationException
  {
    public CRMIntergrationException(
      string message) : base(message)
    {
    }

    public CRMIntergrationException(
      string message,
      Exception exception) : base(
      message,
      exception)
    {
    }
  }
}
