using Clinic.Contracts.Doctors.Commands;
using Clinic.Contracts.Doctors.Query;
using Clinic.Contracts.Doctors.Responses;
using Clinic.Infrastructure.CQRS.Dispatcher.Abstracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly ICommandQueryDispatcher _commandQueryDispatcher;

    public DoctorsController(ICommandQueryDispatcher commandQueryDispatcher)
        => _commandQueryDispatcher = commandQueryDispatcher;

    [HttpGet("list")]
    public async Task<IReadOnlyCollection<DoctorResponse>?> GetList(CancellationToken cancellationToken)
        => await _commandQueryDispatcher.SendAsync(
            query: new GetDoctors(),
            cancellationToken: cancellationToken);

    [HttpGet("{doctorId:guid}")]
    public async Task<DoctorResponse> GetDoctor(
        [FromRoute] Guid doctorId,
        CancellationToken cancellationToken
        )
        => await _commandQueryDispatcher.SendAsync(
            query: new GetDoctor(Id: doctorId),
            cancellationToken: cancellationToken);

    [HttpPost]
    public async Task<IActionResult> CreateDoctor(
        [FromBody] CreateDoctor command,
        CancellationToken cancellationToken)
    {
        await _commandQueryDispatcher.SendAsync(
            command: command,
            cancellationToken: cancellationToken);
        return Ok(command.CreatedId);
        //TODO Return Location to GetDoctor endpoint with CreatedId parameter.
    }
}
