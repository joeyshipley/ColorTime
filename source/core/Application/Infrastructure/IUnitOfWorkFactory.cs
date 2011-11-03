namespace Color.Core.Application.Infrastructure
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork BeginTransaction(); 
	}
}