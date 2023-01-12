using Clinic.Domain.MedicalVisits.Entities;
using MedicalVisitTypeContract = Clinic.Contracts.MedicalVisits.Entities.MedicalVisitType;

namespace Clinic.Domain.MedicalVisits.Utilities;

public static class MedicalVisitTypeExtensions
{
    public static MedicalVisitType ToEntity(this MedicalVisitTypeContract contract)
        => contract switch
        {
            MedicalVisitTypeContract.BasicConsultation => MedicalVisitType.BasicConsultation,
            _ => throw new Exception("Medical Visit Type was not recognized.")
        };

    public static MedicalVisitTypeContract ToContract(this MedicalVisitType entity)
        => entity switch
        {
            MedicalVisitType.BasicConsultation => MedicalVisitTypeContract.BasicConsultation,
            _ => throw new Exception("Medical Visit Type was not recognized.")
        };
}
