using Microsoft.EntityFrameworkCore;

namespace Clinic.Domain.Doctors.Entities.Configuration;

internal class DoctorEntityConfiguration :DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().OwnsOne(
            doctor=> doctor.WorkHours, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.OwnsMany(w=>w);
            });
    }
}
