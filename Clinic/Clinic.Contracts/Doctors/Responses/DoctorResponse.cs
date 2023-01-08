using Clinic.Contracts.Doctors.Entities;

namespace Clinic.Contracts.Doctors.Responses;

public record DoctorResponse(
    Guid Id,
    string FirstName,
    string LastName,
    Specialization Specialization,
    string Email,
    string MobilePhone,
    string Description);