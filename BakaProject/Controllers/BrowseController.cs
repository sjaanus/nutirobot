﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakaProject.Controllers {
	public class BrowseController : Controller {

		public ActionResult Index() {
			return View();
		}

		public ActionResult Home() {
			return View();
		}
	}
}