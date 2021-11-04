using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infra.Api;
using Infra.Api.Conventions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.DealManagement.Core.Application.Commands;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class SimulationController : PwcBaseController
  {
    public SimulationController(IMapper mapper, IMediator mediator)
        : base(mediator, mapper)
    {
    }
  }
}
