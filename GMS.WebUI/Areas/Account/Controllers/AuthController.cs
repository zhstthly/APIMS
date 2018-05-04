using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMS.WebUI.Areas.Account.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View(new LoginInfo());
        }

        [HttpPost]
        public ActionResult Login(LoginInfo info)
        {
            if (!ModelState.IsValid)
                return View();
            if(UserCache.Login(info))
                return RedirectToAction("Index", "Admin", new { Area = "APIMS" });
            else
            {
                ModelState.AddModelError("LoginError", "用户名或密码错误");
                return View();
            }
        }
    }
}