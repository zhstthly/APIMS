using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GMS.WebUI.Infrastructure
{
    public class ClassHelper
    {
        public static IEnumerable<string> GetDisplayNames(Type obj)
        {
            List<string> displayNames = new List<string>();
            var properties = obj.GetProperties();
            foreach (var property in properties)
            {
                var displayAttribute = property.GetCustomAttribute<DisplayAttribute>(true);
                if (displayAttribute != null && !string.IsNullOrEmpty(displayAttribute.Name))
                {
                    displayNames.Add(displayAttribute.Name);
                }
            }

            return displayNames;
        }
    }
}