using Clinic.Infrastructure.CQRS.Abstracts.Commands;

namespace Clinic.Contracts.MedicalVisits.Commands;

internal class CreateMedicalVisit : ICreateCommand
{
    public Guid CreatedId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
