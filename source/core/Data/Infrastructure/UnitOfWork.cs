using Color.Core.Application.Infrastructure;
using NHibernate;

namespace Color.Core.Data.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ISession _session;
		private ITransaction _transaction;

		public UnitOfWork(ISession session)
		{
			_session = session;
		}
		
		public void Begin()
		{
			_transaction = _session.BeginTransaction();
		}

		public void Commit()
		{
			_transaction.Commit();
		}

		public void Rollback()
		{
			_transaction.Rollback();
		}

		public void Dispose()
		{
			if (_transaction != null && _transaction.IsActive)
				_transaction.Rollback();
		}
	}
}