using BakaProject.Models;
using BakaProjectDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BakaProject.Principal {
	public class MyPrincipal : GenericPrincipal {

		public MyPrincipal(IIdentity identity, string[] roles)
			: base(identity, roles) {

		}

		public User User { get; set; }
		public bool TeacherAccount  { get; set; }
	}

}