using System;
using ApplicationException = Infra.Common.Models.Exceptions.ApplicationException;

namespace Troupon.Catalog.Core.Domain
{
  public class CrmIntegrationException : ApplicationException
  {
    public CrmIntegrationException(
      string message) : base(message)
    {
    }

    public CrmIntegrationException(
      string message,
      Exception exception) : base(
      message,
      exception)
    {
    }
  }
}
