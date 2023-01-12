using Clinic.Contracts.MedicalVisits.Queries;
using Clinic.Contracts.MedicalVisits.Responses;
using Clinic.Domain.Doctors.Searchers.Abstracts;
using Clinic.Domain.Doctors.Utilities;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;

namespace Clinic.Domain.MedicalVisits.Handlers.Queries;

internal class GetAvailableMedicalVisitsForDateQueryHandler :
    IQueryHandler<GetAvailableMedicalVisitsForDate, IReadOnlyCollection<AvailableMedicalVisits>>
{
    private readonly IDoctorsSearcher _searcher;

    public GetAvailableMedicalVisitsForDateQueryHandler(IDoctorsSearcher searcher)
        => _searcher = searcher;


    public async Task<IReadOnlyCollection<AvailableMedicalVisits>> HandleAsync(
        GetAvailableMedicalVisitsForDate query,
        CancellationToken cancellationToken)
    => await _searcher.GetAvailableMedicalVisitsForDate(
        date: query.Date,
        specialization: query.DoctorSpecialization.ToEntity(),
        cancellationToken: cancellationToken);
}
