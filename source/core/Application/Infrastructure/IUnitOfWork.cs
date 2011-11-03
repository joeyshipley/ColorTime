namespace Color.Core.Application.Infrastructure
{
	public interface IUnitOfWork : System.IDisposable
	{
		 void Begin();
		 void Commit();
		 void Rollback();
	}
}