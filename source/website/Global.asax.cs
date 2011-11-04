using System.Web.Mvc;
using System.Web.Routing;
using Color.Core.Application.Infrastructure;
using Color.Core.Application.Repositories;
using Color.Core.Application.Services;
using Color.Core.Data.Infrastructure;
using Color.Core.Data.Repositories;
using StructureMap;
using website.Infrastructure;

namespace Color.Website
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			SetupViewEngine();
			Routes.RegisterRoutes(RouteTable.Routes);
			InitializeContainer();
		}

		protected void SetupViewEngine()
		{
			ViewEngines.Engines.Clear();
			var engine = new WebFormViewEngine();
			ViewEngines.Engines.Add(engine);
			engine.ViewLocationFormats = new[] {
				"~/Views/{1}/{0}.aspx",
				"~/Views/{1}/Shared/{0}.ascx",
				"~/Views/Shared/{0}.ascx"
			};
			engine.MasterLocationFormats = new[] {
				"~/Views/{1}/{0}.master",
				"~/Views/Shared/{0}.master"
			};
			engine.PartialViewLocationFormats = engine.ViewLocationFormats;
		}

		public void InitializeContainer()
		{
			IContainer container = new Container(c => 
			{
				// Infrastructure
				c.For<IControllerActivator>().Use<StructureMapControllerActivator>();
				c.For<ISettingsProvider>().Use<ConfigManagerSettingsProvider>();
				c.For<ISessionBuilder>().Use<SessionBuilder>();
				c.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
				c.For<IUnitOfWork>().Use<UnitOfWork>();

				// Application
				c.For<IPlayerRepository>().Use<PlayerRepository>();
				c.For<IGameRoundRepository>().Use<GameRoundRepository>();
				c.For<IColorService>().Use<ColorService>();
				c.For<IGameRoundScorerSpecification>().Use<GameRoundScorerSpecification>();
			});
			DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
		}
	}
}