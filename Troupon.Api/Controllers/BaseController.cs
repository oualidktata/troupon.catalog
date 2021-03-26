using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Troupon.Catalog.Core.Application.Events;
using System;

namespace Troupon.Catalog.Service.Api.Controllers
{

    // [Authorize]
    [ApiController]
    [ApplicationExceptionFilter]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IMediator Mediator;
        protected IMapper Mapper;
        public BaseController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); ;
            Mapper = mapper;
            DomainEvents.Mediator = () => mediator;
        }
    }

}
