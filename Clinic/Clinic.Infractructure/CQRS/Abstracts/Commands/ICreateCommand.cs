namespace Clinic.Infrastructure.CQRS.Abstracts.Commands;

public interface ICreateCommand :ICommand
{
    public Guid CreatedId { get; set; }
}
