using BakaProject.Managers;
using BakaProject.Models;
using BakaProject.Principal;
using BakaProjectDomain.Domain;
using BakaProjectDomain.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace BakaProject.Filters {
	public class AuthenticationFilter : IAuthenticationFilter {

		public async Task AuthenticateAsync(HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken) {
			HttpRequestMessage request = context.Request;
			AuthenticationHeaderValue authorization = request.Headers.Authorization;

			var principal = new MyPrincipal(new GenericIdentity(""), null);
			if (authorization != null) {
				using (RepositoryContext _repository = new RepositoryContext()) {
					var user = _repository.Users.Where(a => a.Tokens.Any(token => token.Value == authorization.Parameter)).FirstOrDefault();
					principal.User = user;

					if (user != null) {
						principal.TeacherAccount = user.TeacherAccount.HasValue && user.TeacherAccount.Value;
					}								
				}
			}
			
			context.Principal = principal;
		}

		public System.Threading.Tasks.Task ChallengeAsync(HttpAuthenticationChallengeContext context, System.Threading.CancellationToken cancellationToken) {
			return Task.FromResult(0);
		}

		public bool AllowMultiple {
			get { return false; }
		}
	}
}