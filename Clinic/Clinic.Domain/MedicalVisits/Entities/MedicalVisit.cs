using Clinic.Infrastructure.Persistence;

namespace Clinic.Domain.MedicalVisits.Entities;

public class MedicalVisit : IEntity
{
    public Guid Id { get; }
    public Guid DoctorId { get; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public string PatientPESEL { get; set; }
    public MedicalVisitType Type { get; }
    public string? Description { get; }

    public MedicalVisit(Guid id, Guid doctorId, string patientFirstName, string patientLastName, string patientPESEL, MedicalVisitType type, string? description)
    {
        Id = id;
        DoctorId = doctorId;
        PatientFirstName = patientFirstName;
        PatientLastName = patientLastName;
        PatientPESEL = patientPESEL;
        Type = type;
        Description = description;
    }
}
