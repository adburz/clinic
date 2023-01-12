using Clinic.Contracts.MedicalVisits.Commands;
using Clinic.Domain.MedicalVisits.Entities;
using Clinic.Domain.MedicalVisits.Utilities;

namespace Clinic.Domain.MedicalVisits.Builders;

internal static class MedicalVisitBuilder
{
    internal static MedicalVisit BuildMedicalVisit(this CreateMedicalVisit command)
        => new(
            id: command.CreatedId,
            patientFirstName: command.PatientFirstName,
            patientLastName: command.PatientLastName,
            patientPESEL: command.PatientPESEL,
            doctorId: command.DoctorId,
            type: command.Type.ToEntity(),
            description: command.Description
            );
}
