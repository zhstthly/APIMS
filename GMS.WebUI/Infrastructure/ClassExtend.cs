using System.Web.Script.Serialization;

namespace GMS.WebUI.Infrastructure
{
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