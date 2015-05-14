using BakaProject.Constants;
using BakaProject.Exceptions;
using BakaProject.Models;
using Facebook;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Plus.v1;
using Google.Apis.Plus.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace BakaProject.Managers {
	public class AuthManager {
		public static SocialMediaProfile GetFacebookProfile(string token) {
			var profile = new SocialMediaProfile();
			var client = new FacebookClient(token);
			dynamic result = client.Get("me", new { fields = "id,name,link" });

			profile.Id = result.id;
			profile.Name = result.name;
			profile.Link = result.link;

			return profile;
		}

		public static String AuthenticateFacebook(AuthModel model) {
			if (model.Code == null) {
				throw new InformativeException(ErrorConstants.WAIT_FOR_LOGIN);
			}
			var fb = new FacebookClient();
			var url = HttpContext.Current.Request.Url;
			dynamic result = fb.Get("oauth/access_token", new {
				client_id = model.ClientId,
				client_secret = Constants.Constants.FACEBOOK_SECRET,
				redirect_uri = url.Scheme + "://" + url.Authority + "/",
				code = model.Code
			});
			return result.access_token;
		}

		public static TokenResponse AuthenticateGoogle(AuthModel model) {
			IAuthorizationCodeFlow flow =
			new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer {
				ClientSecrets = new ClientSecrets {
					ClientId = model.ClientId,
					ClientSecret = Constants.Constants.GOOGLE_SECRET
				},
				Scopes = new[] { PlusService.Scope.PlusLogin }
			});
			var url = HttpContext.Current.Request.Url;
			var token = flow.ExchangeCodeForTokenAsync("", model.Code, url.Scheme + "://" + url.Authority,
							CancellationToken.None).Result;
			return token;
		}

		public static SocialMediaProfile GetGoogleProfile(TokenResponse token) {
			var service = new PlusService();
			IAuthorizationCodeFlow flow =
				new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer {
					ClientSecrets = new ClientSecrets {
						ClientId = Constants.Constants.GOOGLE_CLIENT_ID,
						ClientSecret = Constants.Constants.GOOGLE_SECRET
					},
					Scopes = new[] { PlusService.Scope.PlusLogin }
				});

			var credential = new UserCredential(flow, "", token);
			var plusService = new PlusService(new Google.Apis.Services.BaseClientService.Initializer() {
				HttpClientInitializer = credential
			});

			Person me = plusService.People.Get("me").Execute();

			return new SocialMediaProfile() { 
				Id = me.Id,
				Link = me.Url,
				Name = me.DisplayName
			};
		}
	}
}