using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using GMS.Domian.APIMS.Abstract;
using GMS.WebUI.Areas.APIMS.Models;
using GMS.WebUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GMS.WebUI.Areas.APIMS.Controllers
{
    [Route(Name = "API")]
    public class CustomerAPIItemController : Controller
    {
        private readonly IAPIMSRepository repository;

        public CustomerAPIItemController(IAPIMSRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult APIItems(int classificationID)
        {
            var findItem = this.repository.GetClassificationByID(classificationID);
            this.ViewData["ClassificationName"] = findItem.Name;
            this.ViewData["ClassificationDescription"] = findItem.Description;
            var model = this.repository.GetVisiableAPIItemByClassificationID(classificationID).ToList();
            return this.View(model);
        }

        public ActionResult InputParams(int itemID)
        {
            var model = this.repository.GetItemByID(itemID);
            return this.View(model);
        }

        public ActionResult Result(int itemID)
        {
            var model = this.repository.GetItemByID(itemID);
            return this.View(model);
        }

        public ActionResult TypeViewer(int customerClassID, string value, bool edit = false)
        {
            var model = this.repository.GetCustomerClassByID(customerClassID);
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

            this.ViewData["edit"] = edit;
            return this.View(model);
        }

        public ActionResult Demo(int itemID)
        {
            var model = this.repository.GetItemByID(itemID);
            return this.View(model);
        }

        [NonAction]
        public string GetTypeNameByID(int typeID)
        {
            var type = this.repository.GetAPITypeByID(typeID);
            return type == null ? string.Empty : type.Name;
        }

        [NonAction]
        public string GetTypeColorByID(int typeID)
        {
            var type = this.repository.GetAPITypeByID(typeID);
            return type == null ? string.Empty : type.Color;
        }

        public JsonResult CallAPI(APICall call)
        {
            AjaxResult ar = new AjaxResult();
            var url = call.Url;
            var data = ((string[])call.Data)[0];
            JObject jsonObj = null;
            if (call.Type == "GET" && call.Data != null)
            {
                jsonObj = JsonConvert.DeserializeObject<JObject>(data);
            }

            var type = call.Type;
            if (call == null)
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
                        string getParam = string.Empty;
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

            return this.Json(ar);
        }
    }
}