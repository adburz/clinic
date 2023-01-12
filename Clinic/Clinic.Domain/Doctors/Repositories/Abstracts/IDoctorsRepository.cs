using Clinic.Domain.Doctors.Entities;

namespace Clinic.Domain.Doctors.Repositories.Abstracts;

public interface IDoctorsRepository
{
    Task<IReadOnlyCollection<Doctor>> GetDoctors(CancellationToken cancellationToken);
    Task<Doctor> GetDoctor(Guid id, CancellationToken cancellationToken);
    Task AddDoctor(Doctor doctor, CancellationToken cancellationToken);
    Task UpdateDoctor(Doctor doctor, CancellationToken cancellationToken);
    Task DeleteDoctor(Guid doctorId, CancellationToken cancellationToken);
}
