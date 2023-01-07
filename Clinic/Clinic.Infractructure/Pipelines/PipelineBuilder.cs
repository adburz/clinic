using Clinic.Infrastructure.CQRS.Abstracts.Commands;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;

namespace Clinic.Infrastructure.Pipelines;

internal class PipelineBuilder
{
	private readonly IReadOnlyCollection<Type> _predefinedPipelines;
    private readonly IServiceProvider _serviceProvider;

	public PipelineBuilder(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
		_predefinedPipelines = PredefinedPipelines.DefaultPipelineItems;
    }

	public ICommandHandler<TCommand> CommandPipeline<TCommand>(TCommand command, ICommandHandler<TCommand> commandHandler)
		where TCommand:class, ICommand
	{
		var commandType = command.GetType();
		var pipelineHandlers = _predefinedPipelines
			.Select(c => c.MakeGenericType())
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
		var commandType = query.GetType();
		var pipelineHandlers = _predefinedPipelines
			.Select(c => c.MakeGenericType())
			.Select(c => _serviceProvider.GetService(c))
			.Cast<QueryPipelineItem<TQuery, TResponse>>()
			.ToList();

		pipelineHandlers.Aggregate((curr, next) => curr.SetNextPipelineItem(next));
		pipelineHandlers.Last().SetNextHandler(queryHandler);
		return pipelineHandlers.First();
    }
}
