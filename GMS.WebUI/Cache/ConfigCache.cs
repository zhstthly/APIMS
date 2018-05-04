using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMS.WebUI.Cache
{
    public class ConfigCache
    {
        static object addLock = new object();
        private static Dictionary<int,string> systems = new Dictionary<int, string>();
        private static Dictionary<string, string> environments = new Dictionary<string, string>();
        public static Dictionary<int, string> GetSystems()
        {
            lock(addLock)
            {
                if (systems.Count == 0)
                {
                    var obj = ConfigurationManager.GetSection("SystemList") as NameValueCollection;
                    foreach (var item in obj.AllKeys)
                    {
                        systems.Add(int.Parse(item), obj[item]);
                    }
                }
                return systems;
            }
        }

        public static Dictionary<string, string> GetEnvironments()
        {
            if(environments.Count == 0)
            {
                var obj = ConfigurationManager.GetSection("EnvironmentList") as NameValueCollection;
                foreach (var item in obj.AllKeys)
                {
                    environments.Add(item, obj[item]);
                }
            }
            return environments;
        }
    }
}