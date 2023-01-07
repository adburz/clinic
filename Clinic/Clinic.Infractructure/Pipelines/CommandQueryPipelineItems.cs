using Clinic.Infrastructure.CQRS.Abstracts.Commands;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;

namespace Clinic.Infrastructure.Pipelines;

public abstract class CommandPipelineItem<TCommand>: ICommandHandler<TCommand> where TCommand :class, ICommand
{
    protected ICommandHandler<TCommand> NextHandler { get; private set; }
    public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);

    public CommandPipelineItem<TCommand> SetNextPipelineItem(CommandPipelineItem<TCommand> handler)
    {
        NextHandler = handler;
        return handler;
    }

    public void SetNextHandler(ICommandHandler<TCommand> handler)
        => NextHandler = handler;
}

public abstract class QueryPipelineItem<TQuery, TResponse> : IQueryHandler<TQuery, TResponse> where TQuery :class, IQuery<TResponse>
{
    protected IQueryHandler<TQuery, TResponse> NextHandler { get; private set; }
    public abstract Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken);

    public QueryPipelineItem<TQuery, TResponse> SetNextPipelineItem(QueryPipelineItem<TQuery, TResponse> handler)
    {
        NextHandler = handler;
        return handler;
    }

    public void SetNextHandler(IQueryHandler<TQuery, TResponse> handler)
        => NextHandler = handler;
}