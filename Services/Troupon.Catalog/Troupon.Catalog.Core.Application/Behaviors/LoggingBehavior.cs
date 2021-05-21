﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Troupon.Catalog.Core.Application.Behaviors
{
  public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
  {
    private readonly ILogger logger;

    public LoggingBehavior(
      ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
      this.logger = logger;
    }

    public async Task<TResponse> Handle(
      TRequest request,
      CancellationToken cancellationToken,
      RequestHandlerDelegate<TResponse> next)
    {
      var requestName = request.GetType();
      logger.LogDebug($"Before calling {requestName}");
      var response = await next();
      logger.LogDebug($"After calling {requestName}");

      return response;
    }
  }
}
