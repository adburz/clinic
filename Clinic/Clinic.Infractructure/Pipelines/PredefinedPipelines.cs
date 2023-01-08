using Clinic.Infrastructure.Pipelines.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace Clinic.Infrastructure.Pipelines;

internal static class PredefinedPipelines
{
    public static readonly IReadOnlyCollection<Type> DefaultCommandPipelineItems = new[]
    {
        typeof(LogCommandContextPipelineItem<>)
    }.ToImmutableArray();

    public static readonly IReadOnlyCollection<Type> DefaultQueryPipelineItems = new[]
    {
        typeof(LogQueryContextPipelineItem<,>)
    }.ToImmutableArray();

    public static IServiceCollection AddDefaultPipelineItems(this IServiceCollection services)
    {
        foreach (var p in DefaultCommandPipelineItems)
            services.AddScoped(p);
        foreach (var p in DefaultQueryPipelineItems)
            services.AddScoped(p);
        return services;
    }
}
