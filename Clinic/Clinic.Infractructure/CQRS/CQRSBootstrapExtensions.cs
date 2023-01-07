using Clinic.Infrastructure.CQRS.Abstracts.Commands;
using Clinic.Infrastructure.CQRS.Abstracts.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Infrastructure.CQRS;

internal static class CQRSBootstrapExtensions
{
    public static IServiceCollection RegisterCQRS(this IServiceCollection services)
        => services
            .RegisterAllCommandHandlersFromAssemblyContaining<DummyCommandHandler>()
            .RegisterAllQueryHandlersFromAssemblyContaining<DummyQueryHandler>();
}

public record DummyQuery():IQuery<string>;

public record DummyCommand():ICommand;

public class DummyCommandHandler : ICommandHandler<DummyCommand>
{
    public Task HandleAsync(DummyCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(0);
    }
}


public class DummyQueryHandler : IQueryHandler<DummyQuery, string>
{
    public Task<string> HandleAsync(DummyQuery query, CancellationToken cancellationToken)
    {
        return Task.FromResult("");
    }
}