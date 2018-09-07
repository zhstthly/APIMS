using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GMS.Domian.APIMS.Abstract;
using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Infrastructure;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    public class AdminAPITypeController : AdminControllerBase
    {
        private readonly IAPIMSRepository repository;

        public AdminAPITypeController(IAPIMSRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult APIType()
        {
            return this.View(this.repository.GetAPITypes().ToList());
        }

        public ActionResult Add()
        {
            var types = this.repository.GetAPITypes().ToList();
            types.Add(new APIType());
            return this.View("APIType", types);
        }

        [HttpPost]
        [MultiButton(Name = "Save")]
        public ActionResult Save(APIType item)
        {
            this.repository.SaveType(item);
            return this.RedirectToAction("APIType");
        }

        [HttpPost]
        [MultiButton(Name ="Delete")]
        public ActionResult Delete(APIType item)
        {
            this.repository.DeleteType(item);
            return this.RedirectToAction("APIType");
        }

        [NonAction]
        public IList<APIType> GetAllTypes()
        {
            return this.repository.GetAPITypes().ToList();
        }

        public JsonResult GetAllTypesByAjax()
        {
            var types = this.repository.GetAPITypes();
            var typeList = types.Select(type => new
            {
                value = type.ID,
                text = type.Name
            });
            return this.Json(typeList, JsonRequestBehavior.AllowGet);
        }
    }
}