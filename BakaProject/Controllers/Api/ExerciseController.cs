using BakaProject.Managers;
using BakaProject.Models;
using BakaProject.Principal;
using BakaProjectDomain.Domain;
using BakaProjectDomain.Repository;
using BakaProjectDomain.Extensions;
using Facebook;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BakaProject.Exceptions;
using BakaProjectDomain.Domain.Enums;
using BakaProject.Constants;
using System.Web;
using Spire.Pdf;
using System.Threading;
using System.IO;
using System.Net.Http.Headers;
using Spire.Pdf.HtmlConverter;
using System.Drawing;

namespace BakaProject.Controllers {

	[RoutePrefix("api/exercise")]
	public class ExerciseController : ApiController {
		private ILog log = LogManager.GetLogger(typeof(ExerciseController));

		private IRepository _repository;

		public ExerciseController(IRepository repository) {
			_repository = repository;
		}

		
		public object Save(ExerciseModel model) {
			MyPrincipal userPrincipal = User as MyPrincipal;

			if (userPrincipal.User == null) {
				throw new InformativeException(ErrorConstants.PERMISSION_ERROR);
			} else {
				if (model.Title == null || model.Question == null || model.Answer == null || 
					model.Privacy == 0 || model.Tags.Count == 0) {
					throw new InformativeException(ErrorConstants.FILL_ALL_ERROR);
				}
				var user = _repository.Users.Where(a => a.Id == userPrincipal.User.Id).FirstOrDefault();

				var exercise = _repository.Exercises.Where(a => a.Id == model.Id && a.User.Id == userPrincipal.User.Id).FirstOrDefault();
				if (exercise == null) {

					exercise = new Exercise();
					user.Exercises.Add(exercise);
				}
				exercise.Answer = model.Answer;
				exercise.Question = model.Question;
				exercise.Tip = model.Tip;
				exercise.Title = model.Title;
				exercise.Privacy = model.Privacy;

				var tags = exercise.Tags;

				// remove old ones
				var oldTags = tags.Where(r => !model.Tags.Select(a => a.Text).Contains(r.Name)).ToList();
				tags.RemoveAll(r => !model.Tags.Select(a => a.Text).Contains(r.Name));
				oldTags.ToList().ForEach(a => _repository.DeleteTag(a));
				
				// add new ones
				foreach (var tag in model.Tags) {
					if (!tags.Select(a => a.Name).Contains(tag.Text)) {
						tags.Add(new Tag() {
							Name = tag.Text
						});
					}
				}

				_repository.SaveChanges();
			}
			return Request.CreateErrorResponse(HttpStatusCode.OK, "");
		}

		public void Delete(int id) {
			MyPrincipal userPrincipal = User as MyPrincipal;

			if (userPrincipal.User == null) {
				throw new InformativeException(ErrorConstants.PERMISSION_ERROR);
			} else {
				var exercise = _repository.Exercises.Where(a => a.Id == id && a.User.Id == userPrincipal.User.Id).FirstOrDefault();
				if (exercise == null) {
					throw new InformativeException(ErrorConstants.PERMISSION_ERROR);
				}

				_repository.DeleteExercise(exercise);

				_repository.SaveChanges();
			}
		}

		public object Get([FromUri] String[] allowedTags, [FromUri] String[] deniedTags) {
			MyPrincipal userPrincipal = User as MyPrincipal;

			var exercises = _repository.Exercises
				.Where(a => (allowedTags.Count() == 0 || allowedTags.All(tag => a.Tags.Select(dbTag => dbTag.Name).Contains(tag))) &&
							(deniedTags.Count() == 0 || !deniedTags.Any(tag => a.Tags.Select(dbTag => dbTag.Name).Contains(tag))));

			if (userPrincipal.User == null) {
				exercises = exercises.Where(a => a.PrivacyString == Privacy.PUBLIC.ToString());
			} else {
				exercises = exercises.Where(a =>
					a.PrivacyString == Privacy.PUBLIC.ToString() ||
					userPrincipal.TeacherAccount && a.PrivacyString == Privacy.PUBLIC_TO_TEACHERS.ToString());
			}

			var models = exercises.ToList().Select(a => new {
				Id = a.Id,
				Answer = a.Answer,
				Question = a.Question,
				Tip = a.Tip,
				Title = a.Title,
				User = a.User.Name,
				PrivacyString = a.Privacy.GetDescription(),
				Privacy = a.Privacy,
				UserAllowedToEditItem = userPrincipal.User != null && userPrincipal.User.Id == a.User.Id,
				Tags = a.Tags.Select(tag => new TagModel() {Text = tag.Name }).ToList(),
				TagsText = string.Join(", ", a.Tags.Select(tag => tag.Name))
			}).ToList();
			return models;
		}
		[HttpPost]
		[Route("share")]
		public object Share(ExerciseModel model) {
			var shareUrl = CreateNewShare(model);
			return Request.CreateErrorResponse(HttpStatusCode.OK, shareUrl);
		}

		[HttpGet]
		[Route("share/get")]
		public object Share([FromUri] String id) {

			var share = _repository.Shares.Where(a => a.Hash == id).FirstOrDefault();

			if (share == null) {
				throw new InformativeException(ErrorConstants.SHARE_NOT_EXIST);
			}

			var model = new {
				Id = share.Id,
				Answer = share.Answer,
				Question = share.Question,
				Tip = share.Tip,
				Title = share.Title,
				Tags = share.Tags
			};
			return model;
		}
		[HttpPost]
		[Route("share/file")]
		public object ShareFile(ExerciseModel model) {
			log.Info("share");
			var shareUrl = CreateNewShare(model);

			PdfDocument doc = new PdfDocument();
			log.Info("doc " + shareUrl);
			var settings = new PdfPageSettings();
			settings.Width = 750f;
			settings.Orientation = Spire.Pdf.PdfPageOrientation.Portrait;
			Thread thread = new Thread(() => {
				doc.LoadFromHTML(shareUrl, false, true, true, settings);
			});
			
			log.Info("share 1");
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			log.Info("share 2");
			thread.Join();
			log.Info("share 3");
			//var stream = new MemoryStream();
			//doc.SaveToStream(stream, FileFormat.PDF);
			doc.SaveToFile("C:/Uploads/PDF/test.pdf");
			log.Info("share4 ");
			HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
			//response.Content = new StreamContent(stream);
			response.Content = new StreamContent(new FileStream("C:/Uploads/PDF/test.pdf", FileMode.Open, FileAccess.Read));
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
			log.Info("share 5");
			return response;
		}

		private string CreateNewShare(ExerciseModel model) {
			MyPrincipal userPrincipal = User as MyPrincipal;
			var hash = Guid.NewGuid().ToString();

			if (userPrincipal.User == null) {
				throw new InformativeException(ErrorConstants.PERMISSION_ERROR);
			} else {
				var user = _repository.Users.Where(a => a.Id == userPrincipal.User.Id).FirstOrDefault();

				var share = new Share();

				share.Answer = model.Answer;
				share.Question = model.Question;
				share.Tip = model.Tip;
				share.Title = model.Title;
				if (model.Tags != null && model.Tags.Count != 0) {
					share.Tags = model.Tags.Select(a => a.Text).Aggregate((i, j) => i + " " + j);
				}				
				share.Hash = hash;


				user.Shares.Add(share);
				_repository.SaveChanges();
			}

			var url = HttpContext.Current.Request.Url;
			var shareUrl = url.Scheme + "://" + url.Authority + "/share?id=" + hash;
			return shareUrl;
		}
	}
}
