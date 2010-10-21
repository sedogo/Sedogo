using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace RestAPI
{
    public static class Extensions
    {
        /// <summary>
        /// Makes dictionary from anonymous type. The name of properties with '_' symbol at start of a name will be trimmed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary<T>(this T obj)
        {
            var properties = typeof(T).GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);
            var jsonDict = properties.ToDictionary(propertyInfo => propertyInfo.Name.Trim('_'), propertyInfo => propertyInfo.GetValue(obj, null));
            return jsonDict;
        }
    }
}