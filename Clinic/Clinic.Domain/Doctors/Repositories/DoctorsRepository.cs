using Clinic.Domain.DbContexts;
using Clinic.Domain.Doctors.Entities;
using Clinic.Domain.Doctors.Repositories.Abstracts;
using Clinic.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Domain.Doctors.Repositories;

internal class DoctorsRepository : IDoctorsRepository
{
    private readonly ClinicDbContext _dbContext;

    public DoctorsRepository(ClinicDbContext dbContext)
        => _dbContext = dbContext;

    public async Task AddDoctor(Doctor doctor, CancellationToken cancellationToken)
    {
        await _dbContext.Set<Doctor>().AddAsync(doctor, cancellationToken);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Doctor> GetDoctor(Guid id, CancellationToken cancellationToken)
    {
        var set = _dbContext.Set<Doctor>();
        var doctor = set.Local.SingleOrDefault(c => c.Equals(id)) ??
            await set.SingleOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);

        if (doctor is null)
            throw new EntityNotFoundException(id: id);

        return doctor;
    }

    public async Task<IReadOnlyCollection<Doctor>> GetDoctors(CancellationToken cancellationToken)
        => await _dbContext.Set<Doctor>().ToListAsync(cancellationToken);

    public async Task UpdateDoctor(Doctor doctor, CancellationToken cancellationToken)
    {
        var set = _dbContext.Set<Doctor>();
        set.Update(doctor);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteDoctor(Guid doctorId, CancellationToken cancellationToken)
    {
        var set = _dbContext.Set<Doctor>();
        var doctor = set.Local.SingleOrDefault(c => c.Equals(doctorId)) ??
            await set.SingleOrDefaultAsync(c => c.Id == doctorId, cancellationToken: cancellationToken);

        if (doctor is null)
            throw new EntityNotFoundException(id: doctorId);

        set.Remove(doctor);
        await _dbContext.SaveChangesAsync();
    }
}
