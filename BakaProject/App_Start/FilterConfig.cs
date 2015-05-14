using BakaProject.Filters;
using System.Web;
using System.Web.Mvc;

namespace BakaProject {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}
	}
}
