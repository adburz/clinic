using Clinic.Contracts.Doctors.Commands;
using Clinic.Domain.MedicalVisits.Entities;

namespace Clinic.Domain.Doctors.Utilities;

internal static class DoctorMedicalVisitsBuilder
{
    private const double HoursFromMidnightToStartWorkDay = 8;
    private const double MinutesIntervalBetweenMedicalVisits = 30;

    internal static Dictionary<DateTimeOffset, MedicalVisit?> GetNewMedicalVisits(this AddWorkDay addWorkDay)
    {
        var workDayDate = addWorkDay.WorkDay.Date;

        var defaultWorkingHours = new Dictionary<DateTimeOffset, MedicalVisit?>();

        for (var i = 0; i < 16; i++)
        {
            var key = workDayDate
                .AddHours(HoursFromMidnightToStartWorkDay)
                .AddMinutes(i * MinutesIntervalBetweenMedicalVisits);
            defaultWorkingHours.Add(key, null);
        }
        return defaultWorkingHours;
    }
}
