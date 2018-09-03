using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GMS.WebUI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultiButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }

        public string Argument { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var key = this.ButtonKeyFrom(controllerContext);
            var keyIsValid = IsValid(key);
            if (keyIsValid)
            {
                this.UpdateValueProvider(controllerContext, ValueFrom(key));
            }

            return keyIsValid;
        }

        private static bool IsValid(string key)
        {
            return key != null;
        }

        private static string ValueFrom(string key)
        {
            var parts = key.Split(":".ToCharArray());
            return parts.Length < 2 ? null : parts[1];
        }

        private string ButtonKeyFrom(ControllerContext controllerContext)
        {
            var keys = controllerContext.HttpContext.Request.Params.AllKeys;
            return keys.FirstOrDefault(this.KeyStartsWithButtonName);
        }

        private void UpdateValueProvider(ControllerContext controllerContext, string value)
        {
            if (string.IsNullOrEmpty(this.Argument))
            {
                return;
            }

            ValueProviderResult argument = controllerContext.Controller.ValueProvider.GetValue(this.Argument);
            argument = new ValueProviderResult(value, value, null);
        }

        private bool KeyStartsWithButtonName(string key)
        {
            return key.StartsWith(this.Name, StringComparison.InvariantCulture);
        }
    }
}