using System;
using Infra.Exceptions.ExceptionHandling;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Troupon.Catalog.Api.Conventions;

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
    public ActionResult<ProblemDetails> Error() => handler.Handle(GrabError());

    public Exception? GrabError() => HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;
  }
}
