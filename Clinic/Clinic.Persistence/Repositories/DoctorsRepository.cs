using Clinic.Domain.Doctors;
using Clinic.Domain.Doctors.Entities;
using Clinic.Domain.Exceptions;
using Clinic.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Persistence.Repositories;

internal class DoctorsRepository : IDoctorsRepository
{
    private readonly DbSet<Doctor> _doctors;
    
    public DoctorsRepository(ClinicDbContext dbContext)
        => _doctors = dbContext.Set<Doctor>();

    public async Task AddDoctor(Doctor doctor, CancellationToken cancellationToken)
    {
        doctor.Id = Guid.NewGuid();
        await _doctors.AddAsync(doctor, cancellationToken);
    }

    public async Task<Doctor> GetDoctor(Guid id, CancellationToken cancellationToken)
    {
        var doctor = _doctors.Local.SingleOrDefault(c=>c.Equals(id)) ?? 
            await _doctors.SingleOrDefaultAsync(c=>c.Id == id, cancellationToken: cancellationToken);

        if (doctor is null)
            throw new EntityNotFoundException(id: id);

        return doctor;
    }
}
