using System;
using AutoMapper;
using Infra.MediatR.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [ApplicationExceptionFilter]
  [Route("api/[controller]")]
  public class BaseController : ControllerBase
  {
    protected IMediator Mediator { get; }

    protected IMapper Mapper { get; }

    public BaseController(IMediator mediator, IMapper mapper)
    {
      Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      Mapper = mapper;
      DomainEvents.Mediator = () => mediator;
    }
  }
}
