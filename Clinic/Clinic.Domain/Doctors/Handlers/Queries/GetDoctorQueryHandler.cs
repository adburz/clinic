using Clinic.Contracts.Doctors.Query;
using Clinic.Contracts.Doctors.Responses;
using Clinic.Domain.Doctors.Entities;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;
using SpecializationContract = Clinic.Contracts.Doctors.Entities.Specialization;

namespace Clinic.Domain.Doctors.Handlers.Queries;

internal class GetDoctorQueryHandler : IQueryHandler<GetDoctor, DoctorResponse>
{
    private readonly IDoctorsRepository _doctorsRepository;

    public GetDoctorQueryHandler(IDoctorsRepository doctorsRepository)
        => _doctorsRepository = doctorsRepository;

    public async Task<DoctorResponse> HandleAsync(GetDoctor query, CancellationToken cancellationToken)
    {
        var doctor = await _doctorsRepository.GetDoctor(id: query.Id, cancellationToken: cancellationToken);

        //TODO move to AutoMapper
        var specialization = doctor.Specialization switch
        {
            Specialization.GeneralPractitioner => SpecializationContract.GeneralPractitioner,
            _ => throw new Exception("Specialization was not recognized."),
        };

        return new DoctorResponse(
                Id: doctor.Id,
                FirstName: doctor.FirstName,
                LastName: doctor.LastName,
                Specialization: specialization,
                Email: doctor.Email,
                MobilePhone: doctor.MobilePhone,
                Description: doctor.Description);
    }
}
