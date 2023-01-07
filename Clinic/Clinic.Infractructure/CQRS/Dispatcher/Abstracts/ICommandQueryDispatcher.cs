using Clinic.Infrastructure.CQRS.Abstracts.Commands;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;

namespace Clinic.Infrastructure.CQRS.Dispatcher.Abstracts;

public interface ICommandQueryDispatcher
{
    Task SendAsync(ICommand command, CancellationToken cancellationToken);
    Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken);
}
