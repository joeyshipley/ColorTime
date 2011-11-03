using System;
using System.Linq;
using Color.Core.Domain.Infrastructure;
using NHibernate;
using NHibernate.Linq;

namespace Color.Core.Data.Infrastructure
{
	public abstract class BaseRepository<T> : IBaseRepository<T>
		where T : BaseEntity
	{
		private readonly ISessionBuilder _sessionBuilder;
		private readonly ISession _session;

		protected BaseRepository(ISessionBuilder sessionBuilder)
		{
			_sessionBuilder = sessionBuilder;
			_session = _sessionBuilder.GetSession();
		}

		public IQueryable<T> GetAll()
		{
			return _session.Query<T>();
		}

		public T Get(Guid id)
		{
			return GetAll().SingleOrDefault(p => p.Id == id);
		}

		public void Save(T player)
		{
			_session.Save(player);
		}

		public void Delete(T player)
		{
			_session.Delete(player);
		}
	}
}