using BakaProject.Constants;
using BakaProject.Exceptions;
using BakaProjectDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakaProject.Controllers {
	public class ShareController : Controller {
		private IRepository _repository;

		public ShareController(IRepository repository) {
			_repository = repository;
		}
		public ActionResult Index() {
			string id = Request.QueryString["id"];
			var share = _repository.Shares.Where(a => a.Hash == id).FirstOrDefault();

			if (share == null) {
				throw new InformativeException(ErrorConstants.SHARE_NOT_EXIST);
			}


			ViewBag.Id = share.Id;
			ViewBag.Answer = share.Answer;
			ViewBag.Question = share.Question;
			ViewBag.Tip = share.Tip;
			ViewBag.Title = share.Title;
			ViewBag.Tags = share.Tags;

			return View();
		}
	}
}