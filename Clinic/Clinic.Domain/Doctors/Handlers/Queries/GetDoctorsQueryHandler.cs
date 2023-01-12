using Clinic.Contracts.Doctors.Entities;
using Clinic.Contracts.Doctors.Query;
using Clinic.Contracts.Doctors.Responses;
using Clinic.Domain.Doctors.Entities;
using Clinic.Domain.Doctors.Repositories.Abstracts;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;
using SpecializationContract = Clinic.Contracts.Doctors.Entities.Specialization;
using SpecializationDomain = Clinic.Domain.Doctors.Entities.Specialization;

namespace Clinic.Domain.Doctors.Handlers.Queries;

internal class GetDoctorsQueryHandler : IQueryHandler<GetDoctors, IReadOnlyCollection<DoctorResponse>?>
{
    private readonly IDoctorsRepository _repository;

    public GetDoctorsQueryHandler(IDoctorsRepository repository)
        =>_repository = repository;

    public async Task<IReadOnlyCollection<DoctorResponse>?> HandleAsync(GetDoctors query, CancellationToken cancellationToken)
    {
        var result = (await _repository.GetDoctors(cancellationToken)).Select(doctor =>
        {
            var specialization = doctor.Specialization switch
            {
                SpecializationDomain.GeneralPractitioner => SpecializationContract.GeneralPractitioner,
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
        });
        return result?.ToList();
    }
}
