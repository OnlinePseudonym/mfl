using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MFL.Common.Extensions
{
    public static class GenericExtensions
    {
        public static T Merge<T>(this T target, T source)
        {
            typeof(T)
                .GetProperties()
                .Select((PropertyInfo x) => new KeyValuePair<PropertyInfo, object>(x, x.GetValue(source, null)))
                .Where((KeyValuePair<PropertyInfo, object> x) => IsNotNullOrEmpty(x.Value)).ToList()
                .ForEach((KeyValuePair<PropertyInfo, object> x) => x.Key.SetValue(target, x.Value, null));

                return target;
        }

        private static bool IsNotNullOrEmpty(object obj)
        {
            var enumerable = obj as IEnumerable;
            if (enumerable != null)
            {
                return enumerable.Cast<object>().Any();
            }
            return false;
        }
    }
}
