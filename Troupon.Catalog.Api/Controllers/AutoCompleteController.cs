using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public class AutoCompleteController : BaseController
  {
    public AutoCompleteController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }

    [HttpGet("locations")]
    public async Task<IEnumerable<string>> FindLocationsAutocomplete(string value)
    {
      return Enumerable.Repeat(value, 5);
    }

    [HttpGet("deals")]
    public async Task<IEnumerable<string>> FindDealsAutocomplete(string value)
    {
      return Enumerable.Repeat(value, 5);
    }
  }
}
