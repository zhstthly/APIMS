using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domian.Infrastraucture
{
    public static class ObjExtend
    {
        /// <summary>
        /// 深拷贝，但只支持基本类型拷贝(string,int,bool,DateTime,Guid,float,double,decimal,byte)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceObj"></param>
        /// <param name="targetObj"></param>
        public static void DeepClone<T>(this T sourceObj,T targetObj) where T : BaseModel
        {
            bool equal = true;
            Type type = targetObj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var property in properties)
            {
                if (!(property.PropertyType == typeof(String)
                    || property.PropertyType == typeof(Int32)
                    || property.PropertyType == typeof(Boolean)
                    || property.PropertyType == typeof(Byte)
                    || property.PropertyType == typeof(DateTime)
                    || property.PropertyType == typeof(Guid)
                    || property.PropertyType == typeof(Single)
                    || property.PropertyType == typeof(Double)
                    || property.PropertyType == typeof(Decimal)))
                    continue;
                    
                var targetValue = property.GetValue(targetObj, null);
                if(property.GetValue(sourceObj) != targetValue)
                {
                    property.SetValue(sourceObj, targetValue);
                    equal = false;
                }  
            }
            if(!equal)
                sourceObj.ModifyTime = DateTime.Now;
        }
    }
}
