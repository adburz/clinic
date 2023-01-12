using Clinic.Contracts.Doctors.Commands;
using Clinic.Domain.Doctors.Repositories.Abstracts;
using Clinic.Infrastructure.CQRS.Abstracts.Commands;

namespace Clinic.Domain.Doctors.Handlers.Commands;

internal class DeleteDoctorCommandHandler : ICommandHandler<DeleteDoctor>
{
    private readonly IDoctorsRepository _repository;

    public DeleteDoctorCommandHandler(IDoctorsRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteDoctor command, CancellationToken cancellationToken)
    => await _repository.DeleteDoctor(doctorId: command.DoctorId, cancellationToken: cancellationToken);
}
