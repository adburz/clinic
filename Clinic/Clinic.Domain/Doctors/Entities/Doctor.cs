using Clinic.Infrastructure.Persistence;

namespace Clinic.Domain.Doctors.Entities;

public class Doctor :IEntity
{
    public Guid Id { get;}
    public string FirstName { get;  }
    public string LastName { get; }
    public string FullName { get { return $"{FirstName} {LastName}"; } }
    public Specialization Specialization { get;  }
    public string Email { get; }
    public string MobilePhone { get;  }
    public string Description { get;  }
    public IEnumerable<WorkHour> WorkHours{ get; }

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