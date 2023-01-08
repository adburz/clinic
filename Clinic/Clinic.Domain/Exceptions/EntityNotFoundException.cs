namespace Clinic.Domain.Exceptions;

public class EntityNotFoundException :Exception
{
	public EntityNotFoundException(Guid id) :base($"Entity {id} was not found."){}
}
