using Clinic.Infrastructure.CQRS.Abstracts.Queries;
using Microsoft.Extensions.Logging;

namespace Clinic.Infrastructure.Pipelines.Logging;

internal class LogQueryContextPipelineItem<TQuery, TResponse> : QueryPipelineItem<TQuery, TResponse> 
    where TQuery:class, IQuery<TResponse>
{
    private readonly ILogger<LogQueryContextPipelineItem<TQuery, TResponse>> _logger;

    public LogQueryContextPipelineItem(ILogger<LogQueryContextPipelineItem<TQuery, TResponse>> logger)
        => _logger = logger;

    public async override Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug(string.Format("Dispatching query {0}", typeof(TQuery).Name));

        var response = await NextHandler.HandleAsync(query, cancellationToken);

        if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug(string.Format("Finished dispatching query {0}", typeof(TQuery).Name));
        
        return response;    
    }
}
