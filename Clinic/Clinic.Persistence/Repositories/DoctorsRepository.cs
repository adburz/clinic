using Clinic.Domain.Doctors;
using Clinic.Domain.Doctors.Entities;
using Clinic.Domain.Exceptions;
using Clinic.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Persistence.Repositories;

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
        var doctor = set.Local.SingleOrDefault(c=>c.Equals(id)) ?? 
            await set.SingleOrDefaultAsync(c=>c.Id == id, cancellationToken: cancellationToken);

        if (doctor is null)
            throw new EntityNotFoundException(id: id);

        return doctor;
    }

    public async Task<IReadOnlyCollection<Doctor>> GetDoctors(CancellationToken cancellationToken)
        => await _dbContext.Set<Doctor>().ToListAsync(cancellationToken);
}
