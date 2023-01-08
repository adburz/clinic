using Clinic.Contracts.Doctors.Responses;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;

namespace Clinic.Contracts.Doctors.Query;

public record GetDoctors() :IQuery<IReadOnlyCollection<DoctorResponse>?>;
