using BakaProject.Exceptions;
using BakaProject.Models;
using BakaProject.Principal;
using BakaProjectDomain.Domain;
using BakaProjectDomain.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BakaProject.Controllers {
	[RoutePrefix("api/user")]
	public class UserController : ApiController {
		private ILog log = LogManager.GetLogger(typeof(UserController));

		private IRepository _repository;

		public UserController(IRepository repository) {
			_repository = repository;
		}

		public object Get() {
			MyPrincipal userPrincipal = User as MyPrincipal;

			var users = _repository.Users.Where(a => !a.TeacherAccount.HasValue).ToList();

			var models = users.Select(a => new {
				a.Id,
				a.UserLink,
				a.Name
			}).ToList();
			return models;
		}

		[HttpGet]
		[Route("info")]
		public object Info() {
			MyPrincipal userPrincipal = User as MyPrincipal;

			return new { 
				userPrincipal.User.Name,
				Teacher = userPrincipal.User.TeacherAccount
			};
		}

		[Route("activate/{id}")]
		public object Activate(int id) {
			MyPrincipal userPrincipal = User as MyPrincipal;
			if (!userPrincipal.User.TeacherAccount.Value) {
				throw new InformativeException("Pole piisavalt õigusi kasutaja staatust muuta.");
			}

			var user = _repository.Users.Where(a => a.Id == id && !a.TeacherAccount.HasValue).FirstOrDefault();
			if (user == null) {
				throw new InformativeException("Selle kasutaja staatust ei saa muuta.");
			}

			user.TeacherAccount = true;
			_repository.SaveChanges();

			return Request.CreateErrorResponse(HttpStatusCode.OK, "");
		}
		[Route("reject/{id}")]
		public object Reject(int id) {
			MyPrincipal userPrincipal = User as MyPrincipal;
			if (!userPrincipal.User.TeacherAccount.Value) {
				throw new InformativeException("Pole piisavalt õigusi.");
			}

			var user = _repository.Users.Where(a => a.Id == id && !a.TeacherAccount.HasValue).FirstOrDefault();
			if (user == null) {
				throw new InformativeException("Seda kasutajat ei saa muuta.");
			}

			user.TeacherAccount = false;
			_repository.SaveChanges();

			return Request.CreateErrorResponse(HttpStatusCode.OK, "");
		}
	}
}