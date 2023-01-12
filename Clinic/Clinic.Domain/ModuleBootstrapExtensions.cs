using Clinic.Domain.DbContexts;
using Clinic.Domain.Doctors.Entities;
using Clinic.Domain.Doctors.Entities.Builder;
using Clinic.Domain.Doctors.Handlers.Commands;
using Clinic.Domain.Doctors.Handlers.Queries;
using Clinic.Domain.Doctors.Repositories;
using Clinic.Domain.Doctors.Repositories.Abstracts;
using Clinic.Domain.Doctors.Searchers;
using Clinic.Domain.Doctors.Searchers.Abstracts;
using Clinic.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Domain;

public static class ModuleBootstrapExtensions
{
    public static IServiceCollection RegisterDomain(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ClinicDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ClinicDbContext"),
                serverOptions => serverOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)))
            .AddScoped<IDoctorsRepository, DoctorsRepository>()
            .AddScoped<IDoctorsSearcher, DoctorsSearcher>()
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