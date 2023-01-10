using Clinic.Infrastructure.Persistence;

namespace Clinic.Domain.MedicalVisits.Entities;

public class MedicalVisit : IEntity
{
    public Guid Id { get; }
    public Guid DoctorId { get; }
    public MedicalVisitType Type { get; }
    public string Description { get; }
    public DateTimeOffset StartDate { get; }
    public DateTimeOffset EndDate { get; }
}
