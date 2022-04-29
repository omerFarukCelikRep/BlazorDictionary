using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlazorDictionary.Api.Application.Extensions;

public static class Registration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assemblies = Assembly.GetExecutingAssembly();

        services.AddMediatR(assemblies);

        services.AddAutoMapper(assemblies);

        services.AddValidatorsFromAssembly(assemblies);

        return services;
    }
}
