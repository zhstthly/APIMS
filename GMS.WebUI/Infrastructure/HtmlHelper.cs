using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMS.WebUI.Infrastructure
{
    public class HtmlHelper
    {
        public static string BoolToString(bool value)
        {
            return value ? "是" : "否";
        }

        public static string GetAPITypeColorByID(int typeID)
        {
            var apiTypeController = DependencyResolver.Current.GetService<Areas.APIMS.Controllers.CustomerAPIItemController>();
            return apiTypeController.GetTypeColorByID(typeID);
        }

        public static string GetAPITypeNameByID(int typeID)
        {
            var apiTypeController = DependencyResolver.Current.GetService<Areas.APIMS.Controllers.CustomerAPIItemController>();
            return apiTypeController.GetTypeNameByID(typeID);
        }

        public static Dictionary<int, string> GetClassificationsBySystem(string systemName)
        {
            Dictionary<int, string> dic_Classification = new Dictionary<int, string>();
            var classificationController = DependencyResolver.Current.GetService<Areas.APIMS.Controllers.AdminAPIClassificationController>();
            foreach (var item in classificationController.GetAPIClassifications2List(systemName))
            {
                dic_Classification.Add(item.ID, item.Name);
            }

            return dic_Classification;
        }

        public static bool CheckClassTypeHaveProperties(int classTypeID)
        {
            var customerClassController = DependencyResolver.Current.GetService<Areas.APIMS.Controllers.AdminCustomerClassController>();
            return customerClassController.GetClassPropertiesCountByClassTypeID(classTypeID) > 0;
        }

        public static string GetClassTypeName(int classTypeID)
        {
            var customerClassController = DependencyResolver.Current.GetService<Areas.APIMS.Controllers.AdminCustomerClassController>();
            return customerClassController.GetTypeNameByID(classTypeID);
        }

        public static Dictionary<int, string> GetCommonClassTypes()
        {
            var customerClassController = DependencyResolver.Current.GetService<Areas.APIMS.Controllers.AdminCustomerClassController>();
            return customerClassController.GetCommonClassTypes();
        }

        public static List<SelectListItem> GetPropertyTypes(int customerClassID, int selectedTypeID)
        {
            var selectItemList = new List<SelectListItem>();
            var customerClassController = DependencyResolver.Current.GetService<Areas.APIMS.Controllers.AdminCustomerClassController>();
            var allTypes = customerClassController.GetRelevantTypes(customerClassID);
            foreach (var item in allTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.ID.ToString()
                };
                if (selectItem.Value == selectedTypeID.ToString())
                {
                    selectItem.Selected = true;
                }

                selectItemList.Add(selectItem);
            }

            return selectItemList;
        }

        public static List<SelectListItem> GetSystemSelectItems(string selectName = null)
        {
            var selectItemList = new List<SelectListItem>();
            Dictionary<int, string> systems = Cache.ConfigCache.GetSystems();
            foreach (var item in systems)
            {
                var selectItem = new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Value
                };
                if (selectItem.Value == selectName)
                {
                    selectItem.Selected = true;
                }

                selectItemList.Add(selectItem);
            }

            return selectItemList;
        }

        public static List<SelectListItem> GetAPITypeSelectItems(int selectName)
        {
            var apitypeList = new List<SelectListItem>();
            var apitypeController = DependencyResolver.Current.GetService<Areas.APIMS.Controllers.AdminAPITypeController>();
            var allApitypes = apitypeController.GetAllTypes();
            foreach (var item in allApitypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.ID.ToString()
                };
                if (selectItem.Value == selectName.ToString())
                {
                    selectItem.Selected = true;
                }

                apitypeList.Add(selectItem);
            }

            return apitypeList;
        }

        public static List<SelectListItem> GetEnvironmentSelectItems(string selectName = null)
        {
            var selectItemList = new List<SelectListItem>();
            Dictionary<string, string> environments = Cache.ConfigCache.GetEnvironments();
            foreach (var item in environments)
            {
                var selectItem = new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key
                };
                if (selectItem.Value == selectName)
                {
                    selectItem.Selected = true;
                }

                selectItemList.Add(selectItem);
            }

            return selectItemList;
        }
    }
}