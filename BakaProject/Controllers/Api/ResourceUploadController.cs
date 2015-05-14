using BakaProject.Constants;
using BakaProject.Exceptions;
using BakaProject.Principal;
using BakaProjectDomain.Domain;
using BakaProjectDomain.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BakaProject.Controllers.Api {

	public class ResourceUploadController : ApiController {

		private ILog log = LogManager.GetLogger(typeof(ResourceUploadController));
		private IRepository _repository;
		//private string root = System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads"); 
		private string root = System.Web.HttpContext.Current.Server.MapPath("~/uploads"); 

		public ResourceUploadController(IRepository repository) {
			_repository = repository;
		}

		public object Get() {
			MyPrincipal userPrincipal = User as MyPrincipal;

			if (userPrincipal.User == null) {
				throw new InformativeException(ErrorConstants.PERMISSION_ERROR);
			} else {
				var user = _repository.Users.Where(a => a.Id == userPrincipal.User.Id).FirstOrDefault();

				return user.Resources.Select(a => new {
					a.Id,
					Link = "Uploads/" + a.Name
				});
			}
		}

		public void Delete(int id) {
			MyPrincipal userPrincipal = User as MyPrincipal;

			if (userPrincipal.User == null) {
				throw new InformativeException(ErrorConstants.PERMISSION_ERROR);
			} else {
				var resource = _repository.Resources.Where(a => a.Id == id && a.User.Id == userPrincipal.User.Id).FirstOrDefault();
				if (resource == null) {
					throw new InformativeException(ErrorConstants.PERMISSION_ERROR);
				}
				var path = root + "/" + resource.Name;
				File.Delete(path);

				_repository.DeleteResource(resource);

				_repository.SaveChanges();
			}
		}

		public Task<HttpResponseMessage> PostFile() {
			MyPrincipal userPrincipal = User as MyPrincipal;

			if (userPrincipal.User == null) {
				throw new InformativeException(ErrorConstants.PERMISSION_ERROR);
			} else {
				HttpRequestMessage request = this.Request;
				if (!request.Content.IsMimeMultipartContent()) {
					throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
				}

				
				var provider = new MultipartFormDataStreamProvider(root);
				log.Info("root " + root);
				var task = request.Content.ReadAsMultipartAsync(provider).
					ContinueWith<HttpResponseMessage>(o => {

						FileInfo finfo = new FileInfo(provider.FileData.First().LocalFileName);

						string guid = Guid.NewGuid().ToString();
						string name = guid + "_" + provider.FileData.First().Headers.ContentDisposition.FileName.Replace("\"", "");
						File.Move(finfo.FullName, Path.Combine(root, guid + "_" + provider.FileData.First().Headers.ContentDisposition.FileName.Replace("\"", "")));

						var user = _repository.Users.Where(a => a.Id == userPrincipal.User.Id).FirstOrDefault();

						user.Resources.Add(new Resource() {
							Name = name
						});

						_repository.SaveChanges();
						return new HttpResponseMessage() {
							Content = new StringContent("File uploaded.")
						};
					}
				);
				return task;
			}
		}
	}
}
