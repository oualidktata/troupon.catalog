using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Troupon.Catalog.Core.Domain.InputModels;
using Troupon.Catalog.Service.Api.Validators;

namespace Troupon.Catalog.Service.Api.DependencyInjectionExtensions
{
    public static class AddFluentValidatonExtensions
    {
        public static IServiceCollection AddFluentValidatonToApplication(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateDealModel>, CreateDealModelValidator>();
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            return services;
        }
    }
}
