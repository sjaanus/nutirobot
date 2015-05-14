using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace BakaProject.IoC {
	public class NinjectDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver {
		private IKernel _kernel;

		public NinjectDependencyResolver(IKernel kernel) {
			_kernel = kernel;
		}

		public IDependencyScope BeginScope() {
			return this;
		}

		public object GetService(Type serviceType) {
			return _kernel.TryGet(serviceType, new IParameter[0]);
		}

		public IEnumerable<object> GetServices(Type serviceType) {
			return _kernel.GetAll(serviceType, new IParameter[0]);
		}

		public void Dispose() {
		}
	}
}