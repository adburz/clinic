using Clinic.Infrastructure.Pipelines.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace Clinic.Infrastructure.Pipelines;

internal static class PredefinedPipelines
{
    public static readonly IReadOnlyCollection<Type> DefaultPipelineItems = new[]
    {
        typeof(LogCommandContextPipelineItem<>),
        typeof(LogQueryContextPipelineItem<,>)
    }.ToImmutableArray();

    public static IServiceCollection AddDefaultPipelineItems(this IServiceCollection services)
    {
        foreach (var p in DefaultPipelineItems)
            services.AddScoped(p);
        return services;
    }
}
