using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.Exceptions.ExceptionHandling;
using Microsoft.AspNetCore.Mvc;

namespace Troupon.Catalog.Core.Domain.Exceptions
{
    class POCDomainExceptionHandler : IDomainExceptionHandler<POCDomainException>
    {
      public ProblemDetails Handle(POCDomainException exception)
      {
        return null;
      }
    }
}
