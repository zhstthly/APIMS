using GMS.Domian.APIMS.Abstract;
using GMS.Domian.APIMS.Entities;
using GMS.WebUI.Areas.APIMS.Models;
using GMS.WebUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    [Route(Name = "API")]
    public class CustomerAPIItemController : Controller
    {
        private IAPIMSRepository repository;
        public CustomerAPIItemController(IAPIMSRepository repository)
        {
            this.repository = repository;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult APIItems(int classificationID)
        {
            var findItem = repository.GetClassificationByID(classificationID);
            ViewData["ClassificationName"] = findItem.Name;
            ViewData["ClassificationDescription"] = findItem.Description;
            var model = repository.GetVisiableAPIItemByClassificationID(classificationID).ToList();
            return View(model);
        }

        public ActionResult InputParams(int itemID)
        {
            var model = repository.GetItemByID(itemID);
            return View(model);
        }

        public ActionResult Result(int itemID)
        {
            var model = repository.GetItemByID(itemID);
            return View(model);
        }

        public ActionResult TypeViewer(int customerClassID, string value, bool edit = false)
        {
            var model = repository.GetCustomerClassByID(customerClassID);
            if (value != null && value.Length > 0 && value.StartsWith("{")
                && value.EndsWith("}"))
            {
                var jsonObj = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(value);
                foreach (var modelProperty in model.Properties)
                {
                    foreach (var property in jsonObj.Children())
                    {
                        if (modelProperty.Name == property.Path)
                        {
                            modelProperty.DefaultValue = jsonObj[property.Path].ToString();
                        }
                    }
                }
            }
            ViewData["edit"] = edit;
            return View(model);
        }

        public ActionResult Demo(int itemID)
        {
            var model = repository.GetItemByID(itemID);
            return View(model);
        }

        [NonAction]
        public string GetTypeNameByID(int typeID)
        {
            var type = repository.GetAPITypeByID(typeID);
            return (type == null?"": type.Name);
        }

        [NonAction]
        public string GetTypeColorByID(int typeID)
        {
            var type = repository.GetAPITypeByID(typeID);
            return (type == null ? "" : type.Color);
        }

        public JsonResult CallAPI(APICall call)
        {
            AjaxResult ar = new AjaxResult();
            var url = call.Url;
            var data = ((string[])call.Data)[0];
            JObject jsonObj = null;
            if (call.Type =="GET" && call.Data != null)
                jsonObj = JsonConvert.DeserializeObject<JObject>(data);
            
            var type = call.Type;
            if(call == null)
            {
                ar.IsSuccess = false;
                ar.Message = "调用异常！";
            }
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = null;
                switch (call.Type)
                {
                    case "GET":
                        string getParam = "";
                        if (jsonObj != null)
                        {
                            getParam += "?";
                            foreach (var property in jsonObj.Children())
                            {
                                getParam += property.Path + "=" + jsonObj[property.Path].ToString() + "&";
                            }
                            getParam = getParam.Substring(0, getParam.Length - 1);
                        }
                        response = client.GetAsync(url + getParam).Result;
                        break;
                    case "POST":
                        response = client.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/json")).Result;
                        break;
                }
                if (response.IsSuccessStatusCode)
                {
                    ar.Data = response.Content.ReadAsStringAsync().Result;
                    ar.IsSuccess = true;
                    ar.Message = "调用成功！";
                }
                else
                {
                    ar.IsSuccess = false;
                    ar.Message = response.Content.ReadAsStringAsync().Result;
                }
            }
            return Json(ar);
        }
    }
}