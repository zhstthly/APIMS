using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            if (LoginInfo == null)
            {
                filterContext.Result = RedirectToAction("Login", "Auth", new { Area = "Account" });
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}