using Clinic.Infrastructure.CQRS.Abstracts.Commands;
using Microsoft.Extensions.Logging;

namespace Clinic.Infrastructure.Pipelines.Logging;

internal class LogCommandContextPipelineItem<TCommand> : CommandPipelineItem<TCommand> where TCommand :class, ICommand
{
    private readonly ILogger<LogCommandContextPipelineItem<TCommand>> _logger;

    public LogCommandContextPipelineItem(ILogger<LogCommandContextPipelineItem<TCommand>> logger)
        => _logger = logger;

    public override async Task HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug(string.Format("Dispatching command {0}", typeof(TCommand).Name));

        await NextHandler.HandleAsync(command, cancellationToken);

        if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug(string.Format("Finished dispatching command {0}", typeof(TCommand).Name));
    }
}
