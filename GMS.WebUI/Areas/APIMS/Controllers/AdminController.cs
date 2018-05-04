using GMS.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    public class AdminController : AdminControllerBase
    {
        // GET: APIMS/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopNavbar()
        {
            return PartialView("_Navbar");
        }
    }
}