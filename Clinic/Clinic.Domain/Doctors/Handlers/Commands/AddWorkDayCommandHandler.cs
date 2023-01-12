using Clinic.Contracts.Doctors.Commands;
using Clinic.Domain.Doctors.Repositories.Abstracts;
using Clinic.Domain.Doctors.Utilities;
using Clinic.Infrastructure.CQRS.Abstracts.Commands;

namespace Clinic.Domain.Doctors.Handlers.Commands;

internal class AddWorkDayCommandHandler : ICommandHandler<AddWorkDay>
{
    private readonly IDoctorsRepository _repository;
    public AddWorkDayCommandHandler(IDoctorsRepository repository)
        => _repository = repository;

    public async Task HandleAsync(AddWorkDay command, CancellationToken cancellationToken)
    {
        if (command.WorkDay.Date < DateTimeOffset.UtcNow.Date)
            throw new Exception("Cannot add Work Day with passed date!");

        var doctor = await _repository.GetDoctor(id: command.DoctorId, cancellationToken: cancellationToken);
        var newMedicalVisits = command.GetNewMedicalVisits();
        doctor.AddWorkDay(workDay: command.WorkDay.Date, medicalVisits: newMedicalVisits);
        await _repository.UpdateDoctor(doctor: doctor, cancellationToken: cancellationToken);
    }
}
