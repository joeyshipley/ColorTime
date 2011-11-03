using NHibernate;

namespace Color.Core.Data.Infrastructure
{
	public interface ISessionBuilder
	{
		ISession GetSession();
	}
}