using Clinic.Contracts.MedicalVisits.Commands;
using Clinic.Domain.Doctors.Repositories.Abstracts;
using Clinic.Domain.MedicalVisits.Builders;
using Clinic.Infrastructure.CQRS.Abstracts.Commands;

namespace Clinic.Domain.MedicalVisits.Handlers.Command;

internal class CreateMedicalVisitCommandHandler : ICommandHandler<CreateMedicalVisit>
{
    private readonly IDoctorsRepository _repository;

    public CreateMedicalVisitCommandHandler(IDoctorsRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreateMedicalVisit command, CancellationToken cancellationToken)
    {
        var doctor = await _repository.GetDoctor(id: command.DoctorId, cancellationToken: cancellationToken);
        command.CreatedId = Guid.NewGuid();
        var medicalVisit = command.BuildMedicalVisit();
        doctor.AddMedicalVisit(date: command.Term, medicalVisit: medicalVisit);
        await _repository.UpdateDoctor(doctor: doctor, cancellationToken: cancellationToken);
    }
}
