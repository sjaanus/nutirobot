using BakaProject.Principal;
using BakaProjectDomain.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BakaProject.Controllers {
	public class SidebarController : ApiController {
		private ILog log = LogManager.GetLogger(typeof(SidebarController));
		
		private IRepository _repository;

		public SidebarController(IRepository repository) {
			_repository = repository;
		}

		public object Get() {
			var data = new List<object> {
				new { 
					text= "Sirvi ülesandeid", 
					route= "browse" }				
			};

			MyPrincipal userPrincipal = User as MyPrincipal;
			if (userPrincipal.User != null) {
				data.Add(new {
					text= "Lisa ülesanne", 
					route= "upload"
				});
				data.Add(new {
					text = "Minu pildid",
					route = "resource"
				});
				if (userPrincipal.User.TeacherAccount.HasValue && userPrincipal.User.TeacherAccount.Value == true) {
					if (_repository.Users.Any(a => !a.TeacherAccount.HasValue)) {
						data.Add(new {
							text = "Vaata õpetajataotlusi",
							route = "activate"
						});
					}
				}				
			}
			

			return Json(data.ToList());
		}
	}
}
