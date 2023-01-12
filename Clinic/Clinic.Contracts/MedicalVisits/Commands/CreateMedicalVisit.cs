using Clinic.Contracts.MedicalVisits.Entities;
using Clinic.Infrastructure.CQRS.Abstracts.Commands;

namespace Clinic.Contracts.MedicalVisits.Commands;

public record CreateMedicalVisit(
    string PatientFirstName,
    string PatientLastName,
    string PatientPESEL,
    string? Description,
    Guid DoctorId,
    DateTimeOffset Term,
    MedicalVisitType Type) : ICreateCommand
{
    public Guid CreatedId { get; set; }
}
