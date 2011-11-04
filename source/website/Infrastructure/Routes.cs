using System.Web.Mvc;
using System.Web.Routing;

namespace Color.Website.Infrastructure
{
	public static class Routes
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*allimages}", new { allimages = @".*\.(jpg|jpeg|gif|png|ico)" });

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}