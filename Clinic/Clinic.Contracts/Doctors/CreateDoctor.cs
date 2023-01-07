using Clinic.Infrastructure.CQRS.Abstracts.Commands;

namespace Clinic.Contracts.Doctors;

public record CreateDoctor(string Name) :ICommand;