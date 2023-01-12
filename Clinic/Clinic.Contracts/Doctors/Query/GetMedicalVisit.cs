using Clinic.Contracts.MedicalVisits.Responses;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;

namespace Clinic.Contracts.Doctors.Query;

public record GetMedicalVisit(Guid DoctorId, Guid MedicalVisitId) : IQuery<MedicalVisitResponse>;