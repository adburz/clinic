namespace Clinic.Contracts.MedicalVisits.Responses;

public record AvailableMedicalVisits(
    Guid DoctorId,
    string DoctorFirstName,
    string DoctorLastName,
    IEnumerable<DateTimeOffset> AvailableTerms
    );