using Clinic.Domain.Doctors.Handlers.Commands;
using Clinic.Domain.Doctors.Handlers.Queries;
using Clinic.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Domain;

public static class ModuleBootstrapExtensions
{
    public static IServiceCollection RegisterDomain(this IServiceCollection services)
        => services
            .RegisterAllCommandHandlersFromAssemblyContaining<CreateDoctorCommandHandler>()
            .RegisterAllQueryHandlersFromAssemblyContaining<GetDoctorQueryHandler>();
}
