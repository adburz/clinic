using Clinic.Infrastructure.CQRS;
using Clinic.Infrastructure.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Infrastructure;

public static class ModuleBootstrapExtensions
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        => services
            .RegisterPipelines()
            .RegisterCQRS();
}
