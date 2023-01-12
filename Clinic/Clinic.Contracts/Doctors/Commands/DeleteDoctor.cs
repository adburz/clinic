using Clinic.Infrastructure.CQRS.Abstracts.Commands;

namespace Clinic.Contracts.Doctors.Commands;

public record DeleteDoctor(Guid DoctorId) : ICommand;