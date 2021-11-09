using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infra.Api;
using Infra.Api.Conventions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Troupon.Catalog.Core.Domain.Dtos;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class CategoriesController : PwcBaseController
  {
    public CategoriesController(IMediator mediator, IMapper mapper)
      : base(mediator, mapper)
    {
    }

    [SwaggerOperation(
      Description = "Get all categories",
      OperationId = "top-categories")]
    [HttpGet]
    public Task<IEnumerable<TopCategoryDto>> Get()
    {
      return Task.FromResult(Enumerable.Repeat(new TopCategoryDto(), 3));
    }
  }
}
