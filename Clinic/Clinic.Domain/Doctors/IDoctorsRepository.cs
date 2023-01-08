using Clinic.Domain.Doctors.Entities;

namespace Clinic.Domain.Doctors;

public interface IDoctorsRepository
{
    Task<Doctor> GetDoctor(Guid id, CancellationToken cancellationToken);
    Task AddDoctor(Doctor doctor, CancellationToken cancellationToken);
}
