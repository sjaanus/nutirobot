using BakaProject.Managers;
using BakaProject.Models;
using BakaProjectDomain.Domain;
using BakaProjectDomain.Domain.Enums;
using BakaProjectDomain.Repository;
using Google.Apis.Auth.OAuth2.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace BakaProject.Controllers {
	[RoutePrefix("auth")]
	public class AuthController : ApiController {

		private IRepository _repository;

		public AuthController(IRepository repository) {
			_repository = repository;
		}

		[Route("facebook")]
		public object Facebook(AuthModel model) {
			var bearer = AuthManager.AuthenticateFacebook(model);
			var profile = AuthManager.GetFacebookProfile(bearer);
			var user = _repository.Users.Where(a => a.AuthenticationId == profile.Id).FirstOrDefault();

			string token = Guid.NewGuid().ToString();
			if (user == null) {
				user = new User() {
					AuthenticationId = profile.Id,
					UserLink = profile.Link,
					Name = profile.Name,
					AuthenticationType = AuthenticationType.FACEBOOK
				};
				_repository.AddUser(user);
			}
			user.Tokens.Add(new Token(token));
			_repository.SaveChanges();
			model.token = token;
			return model;
		}
		[Route("google")]
		public object Google(AuthModel model) {
			var tokenResponse = AuthManager.AuthenticateGoogle(model);
			var profile = AuthManager.GetGoogleProfile(tokenResponse);
			//var profile = new SocialMediaProfile() {
			//	Id = "dgas",
			//	Link = "",
			//	Name = "Janno"
			//};
			var user = _repository.Users.Where(a => a.AuthenticationId == profile.Id).FirstOrDefault();

			string token = Guid.NewGuid().ToString();
			if (user == null) {
				user = new User() {
					AuthenticationId = profile.Id,
					UserLink = profile.Link,
					Name = profile.Name,
					AuthenticationType = AuthenticationType.GOOGLE
				};
				_repository.AddUser(user);
			}
			user.Tokens.Add(new Token(token));
			_repository.SaveChanges();
			model.token = token;
			return model;
		}
	}
}
