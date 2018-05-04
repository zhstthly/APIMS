using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;

namespace GMS.WebUI.Infrastructure
{
    public class ClassHelper
    {
        public static IEnumerable<string> GetDisplayNames(Type obj)
        {
            List<string> displayNames = new List<string>();
            var properties = obj.GetProperties();
            foreach(var property in properties)
            {
                var displayAttribute = property.GetCustomAttribute<DisplayAttribute>(true);
                if (displayAttribute != null && !string.IsNullOrEmpty(displayAttribute.Name))
                    displayNames.Add(displayAttribute.Name);
            }
            return displayNames;
        }
    }

    public static class ClassExtend
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                RecursionLimit = recursionDepth
            };
            return serializer.Serialize(obj);
        }
    }
}