using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using Troupon.Catalog.Api.Conventions;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiVersion("2.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class DealsController : BaseController
  {
    public DealsController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }

    [SwaggerOperation(
      Description = "Returns n filtered deals",
      OperationId = "deals")]
    [HttpGet]
    public Task<IEnumerable<DealDto>> Get([FromQuery] DealsSearchFilter value)
    {
      return Task.FromResult(
        Enumerable.Repeat(new DealDto(), 20));
    }

    [SwaggerOperation(
      Description = "Get deal details by id",
      OperationId = "deals")]
    [HttpGet("{id}")]
    public Task<DealDetailsDto> GetById([BindRequired] Guid id)
    {
      return Task.FromResult(new DealDetailsDto());
    }

    [SwaggerOperation(
     Description = "Get deal details by id",
     OperationId = "deals",
     Tags = new[] { "Example for v2 api" })]
    [MapToApiVersion("2.0")]
    [HttpGet("{id}")]
    public Task<DealDetailsDto> GetByIdV2([BindRequired] Guid id)
    {
      return Task.FromResult(new DealDetailsDto());
    }

    [SwaggerOperation(
     Description = "Returns the popular deals for home page",
     OperationId = "popular-deals")]
    [HttpGet("popular-deals")]
    public Task<IEnumerable<DealDto>> Get()
    {
      return Task.FromResult(Enumerable.Repeat(new DealDto(), 20));
    }

    [SwaggerOperation(
     Description = "Returns the n most recent searches",
     OperationId = "recent-search")]
    [HttpGet("recent-search")]
    public Task<IEnumerable<string>> SearchDeals(string value)
    {
      return Task.FromResult(Enumerable.Repeat(value, 5));
    }
  }
}
