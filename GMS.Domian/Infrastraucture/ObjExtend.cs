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
        /// <typeparam name="T">需要拷贝的类型</typeparam>
        /// <param name="sourceObj">被赋值源</param>
        /// <param name="targetObj">拷贝来自目标</param>
        public static void DeepClone<T>(this T sourceObj, T targetObj)
            where T : BaseModel
        {
            bool equal = true;
            Type type = targetObj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (!(property.PropertyType == typeof(string)
                    || property.PropertyType == typeof(int)
                    || property.PropertyType == typeof(short)
                    || property.PropertyType == typeof(bool)
                    || property.PropertyType == typeof(byte)
                    || property.PropertyType == typeof(DateTime)
                    || property.PropertyType == typeof(Guid)
                    || property.PropertyType == typeof(float)
                    || property.PropertyType == typeof(double)
                    || property.PropertyType == typeof(decimal)))
                {
                    continue;
                }

                var targetValue = property.GetValue(targetObj, null);
                if (property.GetValue(sourceObj) != targetValue)
                {
                    property.SetValue(sourceObj, targetValue);
                    equal = false;
                }
            }

            if (!equal)
            {
                sourceObj.ModifyTime = DateTime.Now;
            }
        }
    }
}
