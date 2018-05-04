using GMS.Domian.APIMS.Abstract;
using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Infrastructure;
using GMS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    public class AdminAPITypeController : AdminControllerBase
    {
        private IAPIMSRepository repository;
        public AdminAPITypeController(IAPIMSRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult APIType()
        {
            return View(repository.GetAPITypes().ToList());
        }

        public ActionResult Add()
        {
            var types = repository.GetAPITypes().ToList();
            types.Add(new APIType());
            return View("APIType", types);
        }

        [HttpPost]
        [MultiButton(Name = "Save")]
        public ActionResult Save(APIType item)
        {
            repository.SaveType(item);
            return RedirectToAction("APIType");
        }

        [HttpPost]
        [MultiButton(Name ="Delete")]
        public ActionResult Delete(APIType item)
        {
            repository.DeleteType(item);
            return RedirectToAction("APIType");
        }

        [NonAction]
        public IList<APIType> GetAllTypes()
        {
            return repository.GetAPITypes().ToList();
        }

        public JsonResult GetAllTypesByAjax()
        {
            var types = repository.GetAPITypes();
            List<BseSelect> bseList = new List<BseSelect>();
            foreach(var item in types)
            {
                bseList.Add(new BseSelect
                {
                    value = item.ID,
                    text = item.Name
                });
            }
            return Json(bseList, JsonRequestBehavior.AllowGet);
        }
    }
}