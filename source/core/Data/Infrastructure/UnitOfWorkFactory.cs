using Color.Core.Application.Infrastructure;

namespace Color.Core.Data.Infrastructure
{
	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		private readonly ISessionBuilder _sessionBuilder;

		public UnitOfWorkFactory(ISessionBuilder sessionBuilder)
		{
			_sessionBuilder = sessionBuilder;
		}

		public IUnitOfWork BeginTransaction()
		{
			var unitOfWork = new UnitOfWork(_sessionBuilder.GetSession());
			unitOfWork.Begin();
			return unitOfWork;
		}
	}
}