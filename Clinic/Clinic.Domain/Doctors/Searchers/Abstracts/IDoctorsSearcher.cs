using Clinic.Contracts.MedicalVisits.Responses;
using Clinic.Domain.Doctors.Entities;

namespace Clinic.Domain.Doctors.Searchers.Abstracts;

internal interface IDoctorsSearcher
{
    Task<IReadOnlyCollection<AvailableMedicalVisits>> GetAvailableMedicalVisitsForDate(DateTimeOffset date, Specialization specialization, CancellationToken cancellationToken);
}
