using Clinic.Contracts.Doctors.Entities;
using Clinic.Contracts.MedicalVisits.Commands;
using Clinic.Contracts.MedicalVisits.Queries;
using Clinic.Contracts.MedicalVisits.Responses;
using Clinic.Infrastructure.CQRS.Dispatcher.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalVisitsController : ControllerBase
{
    private readonly ICommandQueryDispatcher _commandQueryDispatcher;

    public MedicalVisitsController(ICommandQueryDispatcher commandQueryDispatcher)
        => _commandQueryDispatcher = commandQueryDispatcher;

    [HttpGet("forDay")]
    public async Task<IReadOnlyCollection<AvailableMedicalVisits>> GetAvailableMedicalVisitsForDate(
        [FromQuery] DateTimeOffset date,
        [FromQuery] Specialization doctorSpecialization,
        CancellationToken cancellationToken)
        => await _commandQueryDispatcher.SendAsync(
            query: new GetAvailableMedicalVisitsForDate(
                Date: date,
                DoctorSpecialization: doctorSpecialization),
            cancellationToken: cancellationToken);

    [HttpPost]
    public async Task<IActionResult> CreateMedicalVisit(
        [FromBody] CreateMedicalVisit command,
        CancellationToken cancellationToken)
    {
        await _commandQueryDispatcher.SendAsync(
            command: command,
            cancellationToken: cancellationToken);
        return Ok(command.CreatedId);
    }
}
