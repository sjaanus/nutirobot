using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using BakaProject.Exceptions;
using System.Web.Http.Filters;
using System.Net;
using System.Web.Http;
using System.Net.Http;

namespace BakaProject.Filters {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class HandleExceptionAttribute : ExceptionFilterAttribute {
		private ILog logger = LogManager.GetLogger(typeof(HandleExceptionAttribute));

		public override void OnException(HttpActionExecutedContext filterContext) {
			var controllerName = filterContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
			var actionName = filterContext.ActionContext.ActionDescriptor.ActionName;
			var message = "Midagi läks valesti, proovi uuesti";
			var unhandled = true;
			try {
				if (filterContext.Exception is InformativeException) {
					message = filterContext.Exception.Message;
					unhandled = false;
				}
				if (unhandled) {
					logger.Error("[" + controllerName + "][" + actionName + "] Unhandled Exception", filterContext.Exception);
				} else {
					logger.Warn("[" + controllerName + "][" + actionName + "] Informative Exception: " + message);
				}
			} catch (Exception e) {
				logger.Fatal("Exception Handler Error", e);
			} finally {
				var response = new HttpResponseMessage(HttpStatusCode.InternalServerError) {
					ReasonPhrase = message
				};
				response.Headers.Add("ShowError", "true");
				throw new HttpResponseException(response);
			}
		}
	}
}