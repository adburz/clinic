using Clinic.Contracts.Doctors.Entities;
using Clinic.Infrastructure.CQRS.Abstracts.Commands;

namespace Clinic.Contracts.Doctors.Commands;

public record CreateDoctor(
    string FirstName,
    string LastName,
    Specialization Specialization,
    string Email,
    string MobilePhone,
    string Description) : ICreateCommand
{
    public Guid CreatedId { get; set; }
};