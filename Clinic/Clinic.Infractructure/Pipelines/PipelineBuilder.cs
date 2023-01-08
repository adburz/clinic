using Clinic.Infrastructure.CQRS.Abstracts.Commands;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;
using System.Data;

namespace Clinic.Infrastructure.Pipelines;

internal class PipelineBuilder
{
    private readonly IServiceProvider _serviceProvider;

	public PipelineBuilder(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
    }

	public ICommandHandler<TCommand> CommandPipeline<TCommand>(TCommand command, ICommandHandler<TCommand> commandHandler)
		where TCommand:class, ICommand
	{
		var commandType = command.GetType();
		var pipelineHandlers = PredefinedPipelines.DefaultCommandPipelineItems
			.Select(c => c.MakeGenericType(commandType))
			.Select(c => _serviceProvider.GetService(c))
			.Cast<CommandPipelineItem<TCommand>>()
			.ToList();

		pipelineHandlers.Aggregate((curr, next) => curr.SetNextPipelineItem(next));
		pipelineHandlers.Last().SetNextHandler(commandHandler);
		return pipelineHandlers.First();
    }

	public IQueryHandler<TQuery, TResponse> QueryPipeline<TQuery, TResponse>(TQuery query, IQueryHandler<TQuery, TResponse> queryHandler)
		where TQuery: class, IQuery<TResponse>
	{
        var pipelineHandlers = PredefinedPipelines.DefaultQueryPipelineItems
			.Select(c => c.MakeGenericType(query.GetType(), typeof(TResponse)))
			.Select(c => _serviceProvider.GetService(c))
			.Cast<QueryPipelineItem<TQuery, TResponse>>()
			.ToList();

		pipelineHandlers.Aggregate((curr, next) => curr.SetNextPipelineItem(next));
		pipelineHandlers.Last().SetNextHandler(queryHandler);
		return pipelineHandlers.First();
    }
}
