namespace Clinic.Infrastructure.CQRS.Abstracts.Queries;

//CQRS's QueryHandler interface
public interface IQueryHandler<TQuery, TResponse> where TQuery: class, IQuery<TResponse>
{
    Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
