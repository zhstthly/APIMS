using System.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(GMS.WebUI.App_Start.NinjectWebCommon), "RegisterServices")]
namespace GMS.WebUI.App_Start
{
    public static class NinjectWebCommon
    {
        public static void RegisterServices()
        {
            DependencyResolver.SetResolver(new Infrastructure.NinjectDependencyResolver(new Ninject.StandardKernel()));
        }
    }
}