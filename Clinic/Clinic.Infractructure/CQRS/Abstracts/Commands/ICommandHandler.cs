namespace Clinic.Infrastructure.CQRS.Abstracts.Commands;

//CQRS's command handler interface
public interface ICommandHandler<TCommand> where TCommand : class, ICommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}
