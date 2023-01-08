using Clinic.Infrastructure.Persistence;

namespace Clinic.Domain.Doctors.Entities;

public class Doctor :IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get { return $"{FirstName} {LastName}"; } }
    public Specialization Specialization { get; set; }
    public string Email { get; set; }
    public string MobilePhone { get; set; }
    public string Description { get; set; }

    public Doctor(
        Guid id, 
        string firstName, 
        string lastName, 
        Specialization specialization, 
        string email, 
        string mobilePhone, 
        string description)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Specialization = specialization;
        Email = email;
        MobilePhone = mobilePhone;
        Description = description;
    }
}