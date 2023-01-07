using Clinic.Contracts.Doctors;
using Clinic.Infrastructure.CQRS.Abstracts.Commands;

namespace Clinic.Domain.Doctors.Handlers.Commands;

public class CreateDoctorCommandHandler : ICommandHandler<CreateDoctor>
{
    public Task HandleAsync(CreateDoctor command, CancellationToken cancellationToken)
    {
        var hehe = "";
        return Task.FromResult("");
    }
}
