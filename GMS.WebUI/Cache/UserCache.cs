using GMS.Domian.APIMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMS.WebUI.Cache
{
    public class UserCache
    {
        public static bool Login(LoginInfo loginInfo)
        {
            if (loginInfo.UserName == "admin" && loginInfo.Password == "admin")
            {
                SetAdminCache("LoginInfo", loginInfo);
                return true;
            }
            return false;
        }
        public static object GetAdminCache(string name)
        {
            return HttpRuntime.Cache.Get(name);
        }

        private static void RemoveAdminCache(string name)
        {
            if (HttpRuntime.Cache[name] != null)
                HttpRuntime.Cache.Remove(name);
        }

        private static void SetAdminCache(string name,object value)
        {
            HttpRuntime.Cache.Insert(name, value);
        }
    }
}