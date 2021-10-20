using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Troupon.Catalog.Api.Conventions;
using Troupon.Catalog.Api.ErrorHandling;
using static Troupon.Catalog.Api.ErrorHandling.ErrorHandlingExtensions;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class ErrorController : Controller
  {
    private readonly IGenericExceptionHandler handler;

    public ErrorController(IGenericExceptionHandler handler)
    {
      this.handler = handler;
    }

    [Route("error")]
    public ActionResult<ProblemDetails> Error() => handler.Handle(HttpContext);
  }
}
