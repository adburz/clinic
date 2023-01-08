using Clinic.Domain.Doctors.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Persistence.DbContexts;

internal class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }

    public DbSet<Doctor> Doctors { get; set; }
}
