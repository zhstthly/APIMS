using System.Web.Mvc;
using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Cache;

namespace GMS.WebUI.Infrastructure
{
    public class AdminControllerBase : Controller
    {
        public LoginInfo LoginInfo
        {
            get
            {
                return UserCache.GetAdminCache("LoginInfo") as LoginInfo;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (this.LoginInfo == null)
            {
                filterContext.Result = this.RedirectToAction("Login", "Auth", new { Area = "Account" });
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}