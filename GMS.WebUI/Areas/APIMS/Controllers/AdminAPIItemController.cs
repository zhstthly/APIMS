using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GMS.Domian.APIMS.Abstract;
using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Infrastructure;
using GMS.WebUI.Models;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    public class AdminAPIItemController : AdminControllerBase
    {
        private readonly IAPIMSRepository repository;

        public AdminAPIItemController(IAPIMSRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult APIItemMaster()
        {
            return this.View();
        }

        [NonAction]
        public List<APIItem> GetItems(int? classificationId)
        {
            this.ViewData["APIId"] = classificationId;
            return this.repository.GetAllAPIItemByClassificationID(classificationId ?? -1).ToList();
        }

        public ActionResult APIItem(int? classificationId)
        {
            return this.View(this.GetItems(classificationId));
        }

        public ActionResult GetItem(int itemID)
        {
            var item = this.repository.GetItemByID(itemID);
            return this.View("APIItemExtend", item);
        }

        /// <summary>
        /// ajax更新操作
        /// </summary>
        /// <param name="param">新增API项实体参数</param>
        /// <param name="itemID">API项的ID</param>
        /// <returns>新增结果的json</returns>
        [HttpPost]
        public JsonResult AddInputParameter(InputParameter param, int itemID)
        {
            InputParameter newParam = this.repository.AddAPIItemInputParam(param, itemID);
            AjaxResult ar = new AjaxResult();
            if (newParam != null)
            {
                ar.IsSuccess = true;
                ar.Message = "新增成功！";
                ar.Data = newParam;
            }
            else
            {
                ar.IsSuccess = false;
                ar.Message = "新增失败！";
                ar.Data = newParam;
            }

            return this.Json(ar);
        }

        [HttpPost]
        public JsonResult SaveInputParameter(InputParameter param)
        {
            this.repository.SaveAPIItemInputParam(param);
            AjaxResult ar = new AjaxResult
            {
                IsSuccess = true,
                Message = "保存成功！"
            };
            return this.Json(ar);
        }

        [HttpPost]
        public JsonResult DeleteInputParameter(InputParameter param)
        {
            this.repository.DeleteAPIItemInputParam(param);
            AjaxResult ar = new AjaxResult
            {
                IsSuccess = true,
                Message = "删除成功！"
            };
            return this.Json(ar);
        }

        [HttpPost]
        public JsonResult SaveResult(Result result)
        {
            this.repository.SaveAPIItemResult(result);
            AjaxResult ar = new AjaxResult
            {
                IsSuccess = true,
                Message = "保存成功！"
            };
            return this.Json(ar);
        }

        /// <summary>
        /// 自定义组件的ajax操作
        /// </summary>
        /// <param name="classificationId">API类型的ID</param>
        /// <returns>API项json</returns>
        public JsonResult GetAPIItems(int? classificationId)
        {
            return this.Json(this.repository.GetAllAPIItemByClassificationID((int)classificationId).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddAPIItem(int? classificationId)
        {
            this.repository.AddAPIItem(new APIItem
            {
                ClassificationID = (int)classificationId
            });
            var result = this.repository.GetAllAPIItemByClassificationID((int)classificationId).ToList();
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditAPIItem(APIItemBase item)
        {
            var ajaxResult = new AjaxResult { };
            this.repository.SaveItemBase(item);
            ajaxResult.IsSuccess = true;
            ajaxResult.Message = "编辑成功！";
            return this.Json(ajaxResult);
        }

        public JsonResult DeleteAPIItem(int[] ids)
        {
            foreach (int id in ids)
            {
                this.repository.DeleteItem(id);
            }

            var ajaxResult = new AjaxResult
            {
                IsSuccess = true,
                Message = "删除成功！"
            };
            return this.Json(ajaxResult);
        }
    }
}