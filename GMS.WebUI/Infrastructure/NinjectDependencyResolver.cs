using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GMS.Domian.APIMS.Abstract;
using GMS.Domian.APIMS.Concrete;
using Ninject;

namespace GMS.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            this.AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            this.kernel.Bind<IAPIMSRepository>().To<EFAPIMSRepository>();
        }
    }
}