using Clinic.Contracts.Doctors.Entities;
using Clinic.Contracts.MedicalVisits.Responses;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;

namespace Clinic.Contracts.MedicalVisits.Queries;

public record GetAvailableMedicalVisitsForDate(DateTimeOffset Date, Specialization DoctorSpecialization)
    : IQuery<IReadOnlyCollection<AvailableMedicalVisits>>;