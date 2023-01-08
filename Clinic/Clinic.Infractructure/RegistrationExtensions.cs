using Clinic.Infrastructure.CQRS.Abstracts.Commands;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Clinic.Infrastructure;

public static class RegistrationExtensions
{
    public static IServiceCollection RegisterAllCommandHandlersFromAssemblyContaining<TType>(this IServiceCollection services)
        => services.RegisterAllHandlersFromAssemblyContaining<TType>(typeof(ICommandHandler<>));

    public static IServiceCollection RegisterAllQueryHandlersFromAssemblyContaining<TType>(this IServiceCollection services)
        => services.RegisterAllHandlersFromAssemblyContaining<TType>(typeof(IQueryHandler<,>));

    private static IServiceCollection RegisterAllHandlersFromAssemblyContaining<TType>(this IServiceCollection services, Type handlerType)
    {
        var assembly = typeof(TType).GetTypeInfo().Assembly;
        var handlers = assembly.GetTypes()
            .Where(c => 
            !c.IsAbstract && 
            c.IsClass && 
            !c.ContainsGenericParameters&& 
            c.GetInterfaces().Any(v => 
                v.IsGenericType && 
                v.GetGenericTypeDefinition() == handlerType));

        foreach(var h in handlers ?? Array.Empty<Type>())
        {
            var types = h.GetInterfaces().Where(c=>c.GetTypeInfo().GetGenericTypeDefinition() == handlerType);
            foreach (var type in types ?? Array.Empty<Type>())
                services.AddTransient(type, h);
        }
        return services;
    }
}