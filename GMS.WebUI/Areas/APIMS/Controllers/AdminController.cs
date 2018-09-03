using System.Web.Mvc;
using GMS.WebUI.Infrastructure;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    public class AdminController : AdminControllerBase
    {
        // GET: APIMS/Admin
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult TopNavbar()
        {
            return this.PartialView("_Navbar");
        }
    }
}