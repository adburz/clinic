using Clinic.Contracts.Doctors.Commands;
using Clinic.Domain.Doctors.Entities;
using Clinic.Infrastructure.CQRS.Abstracts.Commands;
using SpecializationContract = Clinic.Contracts.Doctors.Entities.Specialization;
using SpecializationEntity = Clinic.Domain.Doctors.Entities.Specialization;

namespace Clinic.Domain.Doctors.Handlers.Commands;

public class CreateDoctorCommandHandler : ICommandHandler<CreateDoctor>
{
    private readonly IDoctorsRepository _repository;

    public CreateDoctorCommandHandler(IDoctorsRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreateDoctor command, CancellationToken cancellationToken)
    {
        //TODO Move to AutoMapper
        var specialization = command.Specialization switch
        {
            SpecializationContract.GeneralPractitioner => SpecializationEntity.GeneralPractitioner,
            _ => throw new Exception("Specialization was not recognized."),
        };

        var createdId = Guid.NewGuid();
        var doctor = new Doctor(
            id: createdId,
            firstName: command.FirstName,
            lastName: command.LastName,
            specialization: specialization,
            email: command.Email,
            mobilePhone: command.MobilePhone,
            description: command.Description,
            workHours: null
            );

        await _repository.AddDoctor(doctor,cancellationToken);
        command.CreatedId = createdId;
    }
}
