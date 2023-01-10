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

}
