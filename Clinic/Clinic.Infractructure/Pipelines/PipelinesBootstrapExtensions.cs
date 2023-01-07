using Clinic.Infrastructure.CQRS.Dispatcher;
using Clinic.Infrastructure.CQRS.Dispatcher.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Infrastructure.Pipelines;

internal static class PipelinesBootstrapExtensions
{
    internal static IServiceCollection RegisterPipelines(this IServiceCollection services)
        => services
            .AddScoped<ICommandQueryDispatcher, CommandQueryDispatcher>()
            .AddScoped<PipelineBuilder>()
            .AddDefaultPipelineItems();
}
