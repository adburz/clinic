using Clinic.Domain.Doctors.Entities;
using SpecializationContract = Clinic.Contracts.Doctors.Entities.Specialization;

namespace Clinic.Domain.Doctors.Utilities;

public static class SpecializationExtensions
{
    public static Specialization ToEntity(this SpecializationContract contract)
    => contract switch
    {
        SpecializationContract.GeneralPractitioner => Specialization.GeneralPractitioner,
        _ => throw new Exception("Specialization type not found")
    };

    public static SpecializationContract ToContract(this Specialization entity)
    => entity switch
    {
        Specialization.GeneralPractitioner => SpecializationContract.GeneralPractitioner,
        _ => throw new Exception("Specialization type not found")
    };
}
