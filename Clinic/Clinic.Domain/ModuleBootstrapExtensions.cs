using Clinic.Domain.Doctors.Entities;
using Clinic.Domain.Doctors.Entities.Builder;
using Clinic.Domain.Doctors.Handlers.Commands;
using Clinic.Domain.Doctors.Handlers.Queries;
using Clinic.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Domain;

public static class ModuleBootstrapExtensions
{
    public static IServiceCollection RegisterDomain(this IServiceCollection services)
        => services
            .RegisterAllCommandHandlersFromAssemblyContaining<CreateDoctorCommandHandler>()
            .RegisterAllQueryHandlersFromAssemblyContaining<GetDoctorQueryHandler>();
}

public static class DbContextModuleBuilderExtensions
{
    public static ModelBuilder RegisterDomain(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().ConfigureDoctor();

        return modelBuilder;
    }
}