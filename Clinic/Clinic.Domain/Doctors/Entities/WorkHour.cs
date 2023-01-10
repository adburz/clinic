namespace Clinic.Domain.Doctors.Entities;

public class WorkHour
{
    public DateTimeOffset Date { get; }
    public DateTimeOffset StartTime { get; }
    public DateTimeOffset EndTime { get; }

    public WorkHour(DateTimeOffset date, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
    }
}
