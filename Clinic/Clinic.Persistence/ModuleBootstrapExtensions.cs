using Clinic.Domain.Doctors;
using Clinic.Persistence.DbContexts;
using Clinic.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Persistence;

public static class ModuleBootstrapExtensions
{
    public static IServiceCollection RegisterPersistence(this IServiceCollection services, IConfiguration configuration) 
        => services.AddDbContext<ClinicDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ClinicDbContext"),
            serverOptions => serverOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null)))
        .AddScoped<IDoctorsRepository, DoctorsRepository>();
}
