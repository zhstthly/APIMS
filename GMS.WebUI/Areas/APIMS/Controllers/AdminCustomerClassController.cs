using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using GMS.Domian.APIMS.Abstract;
using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Cache;
using GMS.WebUI.Infrastructure;
using GMS.WebUI.Models;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    public class AdminCustomerClassController : AdminControllerBase
    {
        private readonly IAPIMSRepository repository;

        public AdminCustomerClassController(IAPIMSRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult ClassMaster()
        {
            return this.View();
        }

        public ActionResult CustomerClass(int classificationId)
        {
            var customerClasses = this.repository.GetAllCustomerClassesByClassificationID(classificationId, false).ToList();
            this.ViewData["classificationID"] = classificationId;
            return this.View(customerClasses);
        }

        [HttpPost]
        public JsonResult AddCustomerClass(int classificationID)
        {
            AjaxResult ar = new AjaxResult();
            var newItem = this.repository.AddCustomerClass(new CustomerClass(), classificationID);
            newItem.Name = newItem.Name ?? string.Empty;
            newItem.Description = newItem.Description ?? string.Empty;
            ar.IsSuccess = true;
            ar.Message = "新增成功！";
            ar.Data = newItem;
            return this.Json(ar);
        }

        [HttpPost]
        public JsonResult SaveCustomerClass(CustomerClass customerClass)
        {
            AjaxResult ar = new AjaxResult();
            this.repository.SaveCustomerClass(customerClass);
            ar.IsSuccess = true;
            ar.Message = "保存成功！";
            return this.Json(ar);
        }

        [HttpPost]
        public JsonResult DeleteCustomerClass(int customerClassID)
        {
            AjaxResult ar = new AjaxResult();
            this.repository.DeleteCustomerClass(customerClassID);
            ar.IsSuccess = true;
            ar.Message = "删除成功！";
            return this.Json(ar);
        }

        [HttpPost]
        public JsonResult SetCommonCustomerClass(int customerClassID)
        {
            AjaxResult ar = new AjaxResult();
            this.repository.SetCommonCustomerClass(customerClassID);
            ar.IsSuccess = true;
            ar.Message = "设置成功！";
            return this.Json(ar);
        }

        public ActionResult ClassProperty(int id)
        {
            var customerClass = this.repository.GetCustomerClassByID(id);
            return this.View(customerClass);
        }

        [NonAction]
        public Dictionary<int, string> GetCommonClassTypes()
        {
            var commonClassTypes = this.repository.GetCommonClassTypes();
            Dictionary<int, string> dic_CommonClassType = new Dictionary<int, string>();
            foreach (var item in commonClassTypes)
            {
                dic_CommonClassType.Add(item.ID, item.Name);
            }

            return dic_CommonClassType;
        }

        [NonAction]
        public string GetTypeNameByID(int id)
        {
            var findItem = this.repository.GetClassTypeByID(id);
            if (findItem == null)
            {
                return "null";
            }
            else
            {
                return findItem.Name;
            }
        }

        [NonAction]
        public int GetClassPropertiesCountByClassTypeID(int id)
        {
            var findItem = this.repository.GetClassTypeByID(id);
            if (findItem == null)
            {
                return 0;
            }
            else
            {
                return findItem.Properties.Count;
            }
        }

        [NonAction]
        public IList<CustomerClass> GetRelevantTypes(int classificationID)
        {
            return this.repository.GetAllCustomerClassesByClassificationID(classificationID, true).ToList();
        }

        /// <summary>
        /// 自定义组件的ajax操作
        /// </summary>
        /// <param name="customerClassID">分类ID</param>
        /// <returns>获取自定义分类json</returns>
        public JsonResult GetClassProperties(int? customerClassID)
        {
            return this.Json(this.repository.GetCustomerClassByID((int)customerClassID).Properties.ToList().OrderBy(p => p.ID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddClassProperty(int? customerClassID)
        {
            this.repository.AddClassProperty(new ClassProperty(), (int)customerClassID);
            return this.GetClassProperties(customerClassID);
        }

        public JsonResult EditClassProperty(ClassProperty item)
        {
            var ajaxResult = new AjaxResult { };
            this.repository.SaveClassProperty(item);
            ajaxResult.IsSuccess = true;
            ajaxResult.Message = "编辑成功！";
            return this.Json(ajaxResult);
        }

        public JsonResult DeleteClassProperty(int[] ids)
        {
            foreach (int id in ids)
            {
                this.repository.DeleteClassProperty(id);
            }

            var ajaxResult = new AjaxResult
            {
                IsSuccess = true,
                Message = "删除成功！"
            };
            return this.Json(ajaxResult);
        }

        public JsonResult GetRelevantTypesByAjax(int classificationID)
        {
            var types = this.GetRelevantTypes(classificationID);
            var typeList = types.Select(type => new
            {
                value = type.ID,
                text = type.Name
            });
            return this.Json(typeList, JsonRequestBehavior.AllowGet);
        }

        public string GetSystems()
        {
            return string.Join(",", ConfigCache.GetSystems().Values);
        }
    }
}