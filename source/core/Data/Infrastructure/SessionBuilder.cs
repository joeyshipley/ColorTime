using System;
using Color.Core.Application.Infrastructure;
using Color.Core.Data.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Inspections;
using NHibernate;
using NHibernate.Cfg;

namespace Color.Core.Data.Infrastructure
{
	public class SessionBuilder : ISessionBuilder
	{
		private readonly ISettingsProvider _settingsProvider;
		private readonly ISessionFactory _sessionFactory;
		private ISession _currentSession;
		
		public SessionBuilder(ISettingsProvider settingsProvider)
		{
			_settingsProvider = settingsProvider;
			_sessionFactory = createSessionFactory();
		}

		public ISession GetSession()
		{
			return getExistingOrNewSession();
		}

		private ISessionFactory createSessionFactory()
		{
			return getConfiguration()
				.BuildSessionFactory();
		}

		private ISession getExistingOrNewSession()
		{
			if (_currentSession == null)
				_currentSession = _sessionFactory.OpenSession();
			else if (!_currentSession.IsOpen)
				_currentSession = _sessionFactory.OpenSession();

			return _currentSession;
		}

		private Configuration getConfiguration()
		{
			return Fluently.Configure()
				.Database(configureDatabase())
				.ProxyFactoryFactory("NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle")
				.Mappings(configureMappings())
				.BuildConfiguration();
		}
		
		private Action<MappingConfiguration> configureMappings()
		{
			return (m => m.FluentMappings.Conventions.Setup(c =>
			{
				c.Add(DefaultLazy.Never());
				c.Add<DefaultTableNameConvention>();
			})
				.Conventions.Add(DefaultAccess.CamelCaseField(CamelCasePrefix.Underscore))
				.AddFromAssemblyOf<PlayerMap>());
		}

		private Func<IPersistenceConfigurer> configureDatabase()
		{
			var dbConnectionString = _settingsProvider.Get("DbConnectionString", "");
			var defaultSchema = _settingsProvider.Get("DbSchema", "dbo");
			return (() => MsSqlConfiguration.MsSql2008
				.ConnectionString(c => c.Is(dbConnectionString))
				.DefaultSchema(defaultSchema)
				.ShowSql());
		}
	}
}