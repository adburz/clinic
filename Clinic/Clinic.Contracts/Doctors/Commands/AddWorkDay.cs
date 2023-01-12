using Clinic.Infrastructure.CQRS.Abstracts.Commands;

namespace Clinic.Contracts.Doctors.Commands;

public record AddWorkDay(Guid DoctorId, DateTimeOffset WorkDay) : ICommand;