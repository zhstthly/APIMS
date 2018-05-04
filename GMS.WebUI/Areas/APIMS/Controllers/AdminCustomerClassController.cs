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
    public class AdminCustomerClassController : AdminControllerBase
    {
        private IAPIMSRepository repository;
        public AdminCustomerClassController(IAPIMSRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult ClassMaster()
        {
            return View();
        }

        public ActionResult CustomerClass(int classificationId)
        {
            var customerClasses = repository.GetAllCustomerClassesByClassificationID(classificationId, false).ToList();
            ViewData["classificationID"] = classificationId;
            return View(customerClasses);
        }

        [HttpPost]
        public JsonResult AddCustomerClass(int classificationID)
        {
            AjaxResult ar = new AjaxResult();
            var newItem = repository.AddCustomerClass(new CustomerClass(), classificationID);
            newItem.Name = newItem.Name ?? "";
            newItem.Description = newItem.Description ?? "";
            ar.IsSuccess = true;
            ar.Message = "新增成功！";
            ar.Data = newItem;
            return Json(ar);
        }

        [HttpPost]
        public JsonResult SaveCustomerClass(CustomerClass customerClass)
        {
            AjaxResult ar = new AjaxResult();
            repository.SaveCustomerClass(customerClass);
            ar.IsSuccess = true;
            ar.Message = "保存成功！";
            return Json(ar);
        }

        [HttpPost]
        public JsonResult DeleteCustomerClass(int customerClassID)
        {
            AjaxResult ar = new AjaxResult();
            repository.DeleteCustomerClass(customerClassID);
            ar.IsSuccess = true;
            ar.Message = "删除成功！";
            return Json(ar);
        }

        [HttpPost]
        public JsonResult SetCommonCustomerClass(int customerClassID)
        {
            AjaxResult ar = new AjaxResult();
            repository.SetCommonCustomerClass(customerClassID);
            ar.IsSuccess = true;
            ar.Message = "设置成功！";
            return Json(ar);
        }

        public ActionResult ClassProperty(int id)
        {
            var customerClass = repository.GetCustomerClassByID(id);
            return View(customerClass);
        }

        [NonAction]
        public Dictionary<int,string> GetCommonClassTypes()
        {
            var commonClassTypes = repository.GetCommonClassTypes();
            Dictionary<int, string> dic_CommonClassType = new Dictionary<int, string>();
            foreach(var item in commonClassTypes)
            {
                dic_CommonClassType.Add(item.ID, item.Name);
            }
            return dic_CommonClassType;
        }

        [NonAction]
        public string GetTypeNameByID(int id)
        {
            var findItem = repository.GetClassTypeByID(id);
            if (findItem == null)
                return "null";
            else
                return findItem.Name;
        }

        [NonAction]
        public int GetClassPropertiesCountByClassTypeID(int id)
        {
            var findItem = repository.GetClassTypeByID(id);
            if (findItem == null)
                return 0;
            else
                return findItem.Properties.Count;
        }

        [NonAction]
        public IList<CustomerClass> GetRelevantTypes(int classificationID)
        {
            return repository.GetAllCustomerClassesByClassificationID(classificationID,true).ToList();
        }


        //自定义组件的ajax操作
        public JsonResult GetClassProperties(int? customerClassID)
        {
            return Json(repository.GetCustomerClassByID((int)customerClassID).Properties.ToList().OrderBy(p => p.ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddClassProperty(int? customerClassID)
        {
            repository.AddClassProperty(new ClassProperty(), (int)customerClassID);
            return GetClassProperties(customerClassID);
        }
        public JsonResult EditClassProperty(ClassProperty item)
        {
            var ajaxResult = new AjaxResult { };
            repository.SaveClassProperty(item);
            ajaxResult.IsSuccess = true;
            ajaxResult.Message = "编辑成功！";
            return Json(ajaxResult);
        }
        public JsonResult DeleteClassProperty(int[] ids)
        {
            foreach (int id in ids)
            {
                repository.DeleteClassProperty(id);
            }
            var ajaxResult = new AjaxResult
            {
                IsSuccess = true,
                Message = "删除成功！"
            };
            return Json(ajaxResult);
        }

        public JsonResult GetRelevantTypesByAjax(int classificationID)
        {
            var types = GetRelevantTypes(classificationID);
            List<BseSelect> bseList = new List<BseSelect>();
            foreach (var item in types)
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