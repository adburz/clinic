using Clinic.Contracts.MedicalVisits.Entities;

namespace Clinic.Contracts.MedicalVisits.Responses;

public record MedicalVisitResponse(
    Guid Id,
    Guid DoctorId,
    string PatientFirstName,
    string PatientLastName,
    string PatientPESEL,
    MedicalVisitType Type,
    string? Description);