using Clinic.Contracts.Doctors.Responses;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;

namespace Clinic.Contracts.Doctors.Query;

public record GetDoctor(Guid Id) : IQuery<DoctorResponse>;