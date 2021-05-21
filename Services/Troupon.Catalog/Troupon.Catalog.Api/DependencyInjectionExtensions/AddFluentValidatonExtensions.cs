using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Troupon.Catalog.Api.Validators;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Api.DependencyInjectionExtensions
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
