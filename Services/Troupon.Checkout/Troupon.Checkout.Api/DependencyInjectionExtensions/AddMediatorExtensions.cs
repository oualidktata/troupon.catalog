using System;
using System.IO;
using Infra.MediatR.Caching.Extensions;
using Infra.MediatR.Logging.Extensions;
using Infra.MediatR.Validation.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Troupon.Checkout.Core.Application.Behaviors;
using Troupon.Checkout.Core.Application.Events;

namespace Troupon.Checkout.Api.DependencyInjectionExtensions
{
  public static class AddMediatorExtensions
  {
    public static IServiceCollection AddMediator(
      this IServiceCollection services)
    {
      //Mediator
      services.AddMediatR(typeof(OrderPlacedEvent).Assembly);

      services.AddMediatRCaching();
      services.AddMediatRLogging();
      services.AddMediatRValidation();

      // TODO: Is this needed??
      services.AddTransient<TextWriter>(sp => new WrappingWriter(Console.Out));
      return services;
    }
  }
}
