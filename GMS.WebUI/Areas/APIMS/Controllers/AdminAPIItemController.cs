using GMS.Domian.APIMS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMS.WebUI.Infrastructure;
using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Models;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    public class AdminAPIItemController : AdminControllerBase
    {
        private IAPIMSRepository repository;
        public AdminAPIItemController(IAPIMSRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult APIItemMaster()
        {
            return View();
        }

        [NonAction]
        public List<APIItem> GetItems(int? classificationId)
        {
            ViewData["APIId"] = classificationId;
            return repository.GetAllAPIItemByClassificationID(classificationId ?? -1).ToList();
        }

        public ActionResult APIItem(int? classificationId)
        {
            return View(GetItems(classificationId));
        }

        public ActionResult GetItem(int itemID)
        {
            var item = repository.GetItemByID(itemID);
            return View("APIItemExtend", item);
        }

        //ajax更新操作
        [HttpPost]
        public JsonResult AddInputParameter(InputParameter param, int ItemID)
        {
            InputParameter newParam = repository.AddAPIItemInputParam(param, ItemID);
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
            return Json(ar);
        }

        [HttpPost]
        public JsonResult SaveInputParameter(InputParameter param)
        {
            repository.SaveAPIItemInputParam(param);
            AjaxResult ar = new AjaxResult
            {
                IsSuccess = true,
                Message = "保存成功！"
            };
            return Json(ar);
        }

        [HttpPost]
        public JsonResult DeleteInputParameter(InputParameter param)
        {
            repository.DeleteAPIItemInputParam(param);
            AjaxResult ar = new AjaxResult
            {
                IsSuccess = true,
                Message = "删除成功！"
            };
            return Json(ar);
        }

        [HttpPost]
        public JsonResult SaveResult(Result result)
        {
            repository.SaveAPIItemResult(result);
            AjaxResult ar = new AjaxResult
            {
                IsSuccess = true,
                Message = "保存成功！"
            };
            return Json(ar);
        }

        //自定义组件的ajax操作
        public JsonResult GetAPIItems(int? classificationId)
        {
            return Json(repository.GetAllAPIItemByClassificationID((int)classificationId).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddAPIItem(int? classificationId)
        {
            repository.AddAPIItem(new APIItem
            {
                ClassificationID = (int)classificationId
            });
            var result = repository.GetAllAPIItemByClassificationID((int)classificationId).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditAPIItem(APIItemBase item)
        {
            var ajaxResult = new AjaxResult { };
            repository.SaveItemBase(item);
            ajaxResult.IsSuccess = true;
            ajaxResult.Message = "编辑成功！";
            return Json(ajaxResult);
        }
        public JsonResult DeleteAPIItem(int[] ids)
        {
            foreach(int id in ids)
            {
                repository.DeleteItem(id);
            }
            var ajaxResult = new AjaxResult
            {
                IsSuccess = true,
                Message = "删除成功！"
            };
            return Json(ajaxResult);
        }
    }
}