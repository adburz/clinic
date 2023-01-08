using Clinic.Infrastructure.CQRS.Abstracts.Commands;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;
using Clinic.Infrastructure.CQRS.Dispatcher.Abstracts;
using Clinic.Infrastructure.Pipelines;

namespace Clinic.Infrastructure.CQRS.Dispatcher;

internal class CommandQueryDispatcher : ICommandQueryDispatcher
{
    private readonly Type CommandHandlerType = typeof(ICommandHandler<>);
    private readonly Type QueryHandlerType = typeof(IQueryHandler<,>);
    private readonly Type AsyncCommandHandlerType = typeof(AsyncCommandHandlerWrapper<>);
    private readonly Type AsyncQueryHandlerType = typeof(AsyncQueryHandlerWrapper<,>);

    private readonly IServiceProvider _services;
    private readonly PipelineBuilder _pipelineBuilder;
    public CommandQueryDispatcher(IServiceProvider services, PipelineBuilder pipelineBuilder)
    {
        _services = services;
        _pipelineBuilder = pipelineBuilder;
    }

    public async Task SendAsync(ICommand command, CancellationToken cancellationToken)
    {
        var commandType = command.GetType();
        var commandHandlerType = CommandHandlerType.MakeGenericType(commandType);
        var asyncCommandHandlerType = AsyncCommandHandlerType.MakeGenericType(commandType);
        
        var handler = _services.GetService(commandHandlerType);
        var wrapper = (AsyncCommandHandlerWrapper)Activator.CreateInstance(asyncCommandHandlerType, handler, _pipelineBuilder)!;
        await wrapper.HandleAsync(command: command, cancellationToken: cancellationToken);
    }

    public async Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken)
    {
        var queryType = query.GetType();
        var responseType = typeof(TResponse);
        var queryHandlerType = QueryHandlerType.MakeGenericType(queryType, responseType);
        var asyncQueryCommandHandler= AsyncQueryHandlerType.MakeGenericType(queryType, responseType);

        var handler = _services.GetService(queryHandlerType);
        var wrapper = (AsyncQueryHandlerWrapper<TResponse>)Activator.CreateInstance(asyncQueryCommandHandler, handler, _pipelineBuilder)!;
        return await wrapper.HandleAsync(query: query, cancellationToken: cancellationToken);
    }

    private abstract class AsyncCommandHandlerWrapper
    {
        public abstract Task HandleAsync(ICommand command, CancellationToken cancellationToken);
    }

    private class AsyncCommandHandlerWrapper<TCommand> :AsyncCommandHandlerWrapper
        where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;
        private readonly PipelineBuilder _pipelineBuilder;

        public AsyncCommandHandlerWrapper(ICommandHandler<TCommand> commandHandler, PipelineBuilder pipelineBuilder)
        {
            _commandHandler = commandHandler;
            _pipelineBuilder = pipelineBuilder;
        }

        public override Task HandleAsync(ICommand command, CancellationToken cancellationToken)
        {
            var c = (TCommand)command;
            return _pipelineBuilder
                .CommandPipeline(command: c, commandHandler: _commandHandler)
                .HandleAsync(command: c, cancellationToken: cancellationToken);
        }
    }

    private abstract class AsyncQueryHandlerWrapper<TResponse> 
    {
        public abstract Task<TResponse> HandleAsync(IQuery<TResponse> query, CancellationToken cancellationToken);
    }

    private class AsyncQueryHandlerWrapper<TQuery, TResponse> : AsyncQueryHandlerWrapper<TResponse>
        where TQuery : class, IQuery<TResponse>
    {
        private readonly IQueryHandler<TQuery,TResponse> _queryHandler;
        private readonly PipelineBuilder _pipelineBuilder;

        public AsyncQueryHandlerWrapper(IQueryHandler<TQuery, TResponse> queryHandler, PipelineBuilder pipelineBuilder)
        {
            _queryHandler = queryHandler;
            _pipelineBuilder = pipelineBuilder;
        }

        public override Task<TResponse> HandleAsync(IQuery<TResponse> query, CancellationToken cancellationToken)
        {
            var c = (TQuery)query;
            return _pipelineBuilder
                .QueryPipeline(query: c, queryHandler: _queryHandler)
                .HandleAsync(query: c, cancellationToken: cancellationToken);
        }
    }

}
