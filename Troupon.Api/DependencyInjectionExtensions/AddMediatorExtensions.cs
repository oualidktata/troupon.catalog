using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Troupon.Catalog.Core.Application.Behaviors;
using Troupon.Catalog.Core.Application.Events;
using Troupon.Catalog.Core.Application.Queries.Deals;
using System;
using System.IO;

namespace Troupon.Catalog.Service.Api.DependencyInjectionExtensions
{
    public static class AddMediatorExtensions
    {
        public static IServiceCollection AddMediatorToApplication(this IServiceCollection services)
        {
              //Mediator
            services.AddMediatR(typeof(GetDealsQuery).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient<INotificationHandler<DealCreatedEvent>, DealCreatedEvent.DealCreatedEventHandler>();
            //services.AddScoped<INotificationHandler<DomainNotification>>(sp => (INotificationHandler<DomainNotification>)sp.GetRequiredService(typeof(DomainNotificationHandler))) ;
            services.AddTransient<TextWriter>(sp => new WrappingWriter(Console.Out));


            //services.AddScoped(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>));
            //services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(GenericRequestPostProcessor<,>));
            return services;
        }
    }
}
