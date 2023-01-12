using Clinic.Domain.MedicalVisits.Entities;
using Clinic.Infrastructure.Persistence;

namespace Clinic.Domain.Doctors.Entities;

public class Doctor : IEntity
{
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public Specialization Specialization { get; }
    public string Email { get; }
    public string MobilePhone { get; }
    public string Description { get; }
    public Dictionary<DateTimeOffset, Dictionary<DateTimeOffset, MedicalVisit?>>? MedicalVisits { get; private set; }

    private Doctor() { }

    public Doctor(
        Guid id,
        string firstName,
        string lastName,
        Specialization specialization,
        string email,
        string mobilePhone,
        string description,
        Dictionary<DateTimeOffset, Dictionary<DateTimeOffset, MedicalVisit?>>? medicalVisits)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Specialization = specialization;
        Email = email;
        MobilePhone = mobilePhone;
        Description = description;
        MedicalVisits = medicalVisits ?? new();
    }

    public void AddWorkDay(DateTimeOffset workDay, Dictionary<DateTimeOffset, MedicalVisit?> medicalVisits)
    {
        if (MedicalVisits is null)
            MedicalVisits = new Dictionary<DateTimeOffset, Dictionary<DateTimeOffset, MedicalVisit?>>();

        if (MedicalVisits.ContainsKey(workDay.Date))
            throw new Exception("Doctor has already defined Medical Visits set for this day.");

        MedicalVisits.Add(workDay, medicalVisits);
    }

    public void AddMedicalVisit(DateTimeOffset date, MedicalVisit medicalVisit)
    {
        if (MedicalVisits is null)
            MedicalVisits = new Dictionary<DateTimeOffset, Dictionary<DateTimeOffset, MedicalVisit?>>();

        if (!MedicalVisits.ContainsKey(date.Date))
            throw new Exception("Doctor is not available in this term.");

        MedicalVisits[date.Date][date] = medicalVisit;
    }
}