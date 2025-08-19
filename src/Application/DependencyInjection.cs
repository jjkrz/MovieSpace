using Application.Abstractions;
using Application.Behaviors;
using Application.Profiles;
using Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient<IApplicationEventDispatcher, ApplicationEventDispatcher>();

            services.AddAutoMapper(typeof(MovieProfile));

            services.AddHostedService<MovieRatingCalculationService>();

            return services;
        }

    }
}
