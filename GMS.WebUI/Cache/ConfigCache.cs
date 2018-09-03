using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace GMS.WebUI.Cache
{
    public class ConfigCache
    {
        private static readonly object AddLock = new object();
        private static Dictionary<int, string> systems = new Dictionary<int, string>();
        private static Dictionary<string, string> environments = new Dictionary<string, string>();

        public static Dictionary<int, string> GetSystems()
        {
            lock (AddLock)
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
            if (environments.Count == 0)
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