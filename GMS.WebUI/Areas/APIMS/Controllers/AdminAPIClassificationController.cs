using GMS.Domian.APIMS.Abstract;
using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    public class AdminAPIClassificationController : AdminControllerBase
    {
        private IAPIMSRepository repository;
        public AdminAPIClassificationController(IAPIMSRepository repository)
        {
            this.repository = repository;
        }
        public ActionResult APIClassification()
        {
            return View(repository.GetAllAPIClassifications().ToList());
        }

        public IList<APIClassification> GetAPIClassifications2List(string system)
        {
            return repository.GetAllAPIClassifications().Where(c => c.BelongSystem == system).ToList();
        }

        public JsonResult GetAPIClassifications2Json(string system)
        {
            return Json(GetAPIClassifications2List(system), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            var items = repository.GetAllAPIClassifications().ToList();
            items.Add(new APIClassification());
            return View("APIClassification", items);
        }

        [HttpPost]
        [MultiButton(Name = "Save")]
        public ActionResult Save(APIClassification item)
        {
            repository.SaveClassification(item);
            return RedirectToAction("APIClassification");
        }

        [HttpPost]
        [MultiButton(Name = "Delete")]
        public ActionResult Delete(APIClassification item)
        {
            repository.DeleteClassification(item);
            return RedirectToAction("APIClassification");
        }
    }
}