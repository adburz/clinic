using Clinic.Contracts.Doctors;
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

    [HttpPost]
    public async Task CreateDoctor(
        [FromRoute] string FirstName,
        [FromRoute] string LastName,
        [FromRoute] Specialization Specialization,
        [FromRoute] string Email,
        [FromRoute] string MobilePhone,
        [FromRoute] string Description,
        CancellationToken cancellationToken)
    {
        await _commandQueryDispatcher.SendAsync(
            command:new CreateDoctor(
                FirstName: FirstName,
                LastName: LastName,
                Specialization: Specialization,
                Email: Email,
                MobilePhone: MobilePhone,
                Description: Description
                ),
            cancellationToken: cancellationToken);

        //TODO Return Location to GetDoctor endpoint with CreatedId parameter.
    }
}
