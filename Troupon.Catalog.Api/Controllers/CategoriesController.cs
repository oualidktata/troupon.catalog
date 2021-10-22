using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Troupon.Catalog.Api.Conventions;
using Troupon.Catalog.Core.Domain.Dtos;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class CategoriesController : BaseController
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
