using System.Web.Mvc;
using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Cache;

namespace GMS.WebUI.Areas.Account.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return this.View(new LoginInfo());
        }

        [HttpPost]
        public ActionResult Login(LoginInfo info)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            if (UserCache.Login(info))
            {
                return this.RedirectToAction("Index", "Admin", new { Area = "APIMS" });
            }
            else
            {
                this.ModelState.AddModelError("LoginError", "用户名或密码错误");
                return this.View();
            }
        }
    }
}