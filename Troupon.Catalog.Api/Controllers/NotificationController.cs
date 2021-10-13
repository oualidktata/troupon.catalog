using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using Troupon.Catalog.Api.Conventions;
using Troupon.Catalog.Core.Application.Queries.Deals;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class NotificationController : BaseController
  {
    public NotificationController(IMapper mapper, IMediator mediator)
      : base(mediator, mapper)
    {
    }

    [SwaggerOperation(
      Description = "Create a notification template",
      OperationId = "CreateNotificationTemplate")]
    [HttpPost]
    public async Task<ActionResult<NotificationDto>> Create([FromBody] CreateNotificationTemplateCommand notification, CancellationToken cancellationToken)
    {
      return Created(string.Empty, new NotificationDto
      {
        Id = "123",
        Name = "ABC",
      });
    }

    [SwaggerOperation(
          Description = "Send a notification to user",
          OperationId = "SendNotification")]
    [HttpPost("send")]
    public async Task<ActionResult<NotificationDto>> Send([FromBody] SendNotificationCommand command, CancellationToken c)
    {
    }

    public class NotificationTemplateIds
    {
      public string SMS { get; set; } = "128734918723";

      public string Email { get; set; }

      public string Push { get; set; }
    }
  }
}
