using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GMS.Domian.APIMS.Abstract;
using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Infrastructure;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    public class AdminAPIClassificationController : AdminControllerBase
    {
        private readonly IAPIMSRepository repository;

        public AdminAPIClassificationController(IAPIMSRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult APIClassification()
        {
            return this.View(this.repository.GetAllAPIClassifications().ToList());
        }

        public IList<APIClassification> GetAPIClassifications2List(string system)
        {
            return this.repository.GetAllAPIClassifications().Where(c => c.BelongSystem == system).ToList();
        }

        public JsonResult GetAPIClassifications2Json(string system)
        {
            return this.Json(this.GetAPIClassifications2List(system), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            var items = this.repository.GetAllAPIClassifications().ToList();
            items.Add(new APIClassification());
            return this.View("APIClassification", items);
        }

        [HttpPost]
        [MultiButton(Name = "Save")]
        public ActionResult Save(APIClassification item)
        {
            this.repository.SaveClassification(item);
            return this.RedirectToAction("APIClassification");
        }

        [HttpPost]
        [MultiButton(Name = "Delete")]
        public ActionResult Delete(APIClassification item)
        {
            this.repository.DeleteClassification(item);
            return this.RedirectToAction("APIClassification");
        }
    }
}