using System.Web;
using System.Web.Optimization;

namespace BakaProject {
	public class BundleConfig {

		public static void RegisterBundles(BundleCollection bundles) {
			bundles.Add(new ScriptBundle("~/bundles/library")
				.IncludeDirectory("~/Scripts/Libraries/jquery", "*.js", true)
				.Include("~/Scripts/Libraries/angularjs/angular.js")
				.IncludeDirectory("~/Scripts/Libraries/angularjs", "*.js", true)
				.Include("~/Scripts/Libraries/kendo/kendo.core.js")
				.IncludeDirectory("~/Scripts/Libraries/kendo", "*.js", true)
				.IncludeDirectory("~/Scripts/Libraries", "*.js", true));

			bundles.Add(new ScriptBundle("~/bundles/application")
				.IncludeDirectory("~/Scripts/Application/Modules", "*.js", true)
				.IncludeDirectory("~/Scripts/Application/Services", "*.js", true)
				.IncludeDirectory("~/Scripts/Application/Controllers", "*.js", true));

			bundles.Add(new StyleBundle("~/Content/libs")
				.IncludeDirectory("~/Content/Libraries/Kendo", "*.css", true)
				.IncludeDirectory("~/Content/Libraries", "*.css", true));

			bundles.Add(new StyleBundle("~/Content/app")
				.IncludeDirectory("~/Content/Application", "*.css", true));
		}
	}
}
