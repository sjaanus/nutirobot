using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BakaProject {
	public class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);

		//	routes.MapRoute(
		//name: "routeOne",
		//url: "home/One",
		//defaults: new { controller = "Home", action = "One" });

		//	routes.MapRoute(
		//	  name: "upload",
		//	  url: "upload",
		//	  defaults: new { controller = "Upload", action = "Index" });

		//	routes.MapRoute(
		//	  name: "browse",
		//	  url: "browse",
		//	  defaults: new { controller = "browse", action = "Index" });

		//	routes.MapRoute(
		//	  name: "Default",
		//	  url: "{*url}",
		//	  defaults: new { controller = "Home", action = "Index" });
		}
	}
}
