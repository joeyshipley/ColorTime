using Color.Core.Application.Infrastructure;
using Color.Core.Data.Infrastructure;

namespace Color.Tests.Infrastructure
{
	public class BaseRepositorySpecs
	{
		protected static ISettingsProvider SettingsProvider = new ConfigManagerSettingsProvider();
		protected static ISessionBuilder SessionBuilder = new SessionBuilder(SettingsProvider);
		protected static IUnitOfWorkFactory UnitOfWorkFactory = new UnitOfWorkFactory(SessionBuilder);
	}
}