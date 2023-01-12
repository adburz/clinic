using Clinic.Contracts.MedicalVisits.Responses;
using Clinic.Domain.DbContexts;
using Clinic.Domain.Doctors.Entities;
using Clinic.Domain.Doctors.Searchers.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Domain.Doctors.Searchers;

internal class DoctorsSearcher : IDoctorsSearcher
{
    private readonly ClinicDbContext _dbContext;

    public DoctorsSearcher(ClinicDbContext dbContext)
    => _dbContext = dbContext;

    public async Task<IReadOnlyCollection<AvailableMedicalVisits>> GetAvailableMedicalVisitsForDate(
        DateTimeOffset date,
        Specialization specialization,
        CancellationToken cancellationToken)
    {
        var results = await _dbContext.Set<Doctor>()
            .Where(c => c.Specialization == specialization)
            .ToListAsync();

        return results.Where(c => c.MedicalVisits != null && c.MedicalVisits.ContainsKey(date.Date)).Select(doctor =>
        {
            var availableTerms = doctor.MedicalVisits![date.Date]
                .Where(c => c.Value is null)
                .Select(c => c.Key);

            return new AvailableMedicalVisits(
            DoctorId: doctor.Id,
            DoctorFirstName: doctor.FirstName,
            DoctorLastName: doctor.LastName,
            AvailableTerms: availableTerms);
        }).ToList();
    }
}
