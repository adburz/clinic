using Clinic.Contracts.Doctors.Query;
using Clinic.Contracts.MedicalVisits.Responses;
using Clinic.Domain.Doctors.Repositories.Abstracts;
using Clinic.Domain.MedicalVisits.Utilities;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;

namespace Clinic.Domain.Doctors.Handlers.Queries;

internal class GetMedicalVisitQueryHandler : IQueryHandler<GetMedicalVisit, MedicalVisitResponse>
{
    private readonly IDoctorsRepository _repository;

    public GetMedicalVisitQueryHandler(IDoctorsRepository repository)
        => _repository = repository;

    public async Task<MedicalVisitResponse> HandleAsync(GetMedicalVisit query, CancellationToken cancellationToken)
    {
        var doctor = await _repository.GetDoctor(id: query.DoctorId, cancellationToken: cancellationToken);

        var visits = doctor.MedicalVisits.Select(c => c.Value);
        var result = visits.SelectMany(c => c.Values)
            .Where(c => c is not null)
            .FirstOrDefault(x => x.Id == query.MedicalVisitId);

        return new MedicalVisitResponse(
            Id: result.Id,
            DoctorId: result.DoctorId,
            PatientFirstName: result.PatientFirstName,
            PatientLastName: result.PatientLastName,
            PatientPESEL: result.PatientPESEL,
            Type: result.Type.ToContract(),
            Description: result.Description);
    }
}
