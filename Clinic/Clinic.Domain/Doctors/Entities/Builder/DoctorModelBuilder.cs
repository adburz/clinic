using Clinic.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Domain.Doctors.Entities.Builder;

internal static class DoctorModelBuilder
{
    public static void ConfigureDoctor(this EntityTypeBuilder<Doctor> entityTypeBuilder)
    {
        entityTypeBuilder.Property(c => c.Id).IsRequired().ValueGeneratedNever();
        entityTypeBuilder.Property(c => c.FirstName).IsRequired();
        entityTypeBuilder.Property(c => c.LastName).IsRequired();
        entityTypeBuilder.Property(c => c.Specialization).IsRequired();
        entityTypeBuilder.Property(c => c.Email).IsRequired();
        entityTypeBuilder.Property(c => c.MobilePhone).IsRequired();
        entityTypeBuilder.Property(c => c.Description).IsRequired();
        entityTypeBuilder.Property(c => c.MedicalVisits).HasJsonConversion();
    }
}
