using Clinic.Contracts.Doctors.Commands;
using Clinic.Contracts.Doctors.Query;
using Clinic.Contracts.Doctors.Responses;
using Clinic.Contracts.MedicalVisits.Responses;
using Clinic.Infrastructure.CQRS.Dispatcher.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly ICommandQueryDispatcher _commandQueryDispatcher;

    public DoctorsController(ICommandQueryDispatcher commandQueryDispatcher)
        => _commandQueryDispatcher = commandQueryDispatcher;

    [HttpGet]
    public async Task<IReadOnlyCollection<DoctorResponse>?> GetList(CancellationToken cancellationToken)
        => await _commandQueryDispatcher.SendAsync(
            query: new GetDoctors(),
            cancellationToken: cancellationToken);

    [HttpGet("{doctorId:guid}/{medicalVisitId:guid}")]
    public async Task<MedicalVisitResponse> GetMedicalVisit(
        [FromRoute] Guid doctorId,
        [FromRoute] Guid medicalVisitId,
        CancellationToken cancellationToken)
        => await _commandQueryDispatcher.SendAsync(
            query: new GetMedicalVisit(DoctorId: doctorId, MedicalVisitId: medicalVisitId),
            cancellationToken: cancellationToken);

    [HttpGet("{doctorId:guid}")]
    public async Task<DoctorResponse> GetDoctor(
        [FromRoute] Guid doctorId,
        CancellationToken cancellationToken)
        => await _commandQueryDispatcher.SendAsync(
            query: new GetDoctor(Id: doctorId),
            cancellationToken: cancellationToken);

    [HttpPost("{doctorId:guid}/workDay")]
    public async Task<IActionResult> AddWorkDay(
        [FromRoute] Guid doctorId,
        [FromBody] DateTimeOffset workDay,
        CancellationToken cancellationToken)
    {
        await _commandQueryDispatcher.SendAsync(
            command: new AddWorkDay(DoctorId: doctorId, WorkDay: workDay),
            cancellationToken: cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor(
        [FromBody] CreateDoctor command,
        CancellationToken cancellationToken)
    {
        await _commandQueryDispatcher.SendAsync(
            command: command,
            cancellationToken: cancellationToken);
        return Created(nameof(GetDoctor), command.CreatedId);
        //TODO Return Location to GetDoctor endpoint with CreatedId parameter.
    }

    [HttpDelete("{doctorId:guid}")]
    public async Task DeleteDoctor(
        [FromRoute] Guid doctorId,
        CancellationToken cancellationToken)
        => await _commandQueryDispatcher.SendAsync(
            command: new DeleteDoctor(DoctorId: doctorId),
            cancellationToken: cancellationToken);
}
