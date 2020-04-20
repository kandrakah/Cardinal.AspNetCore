using System.Dynamic;

namespace Cardinal.AspNetCore.Extensions
{
    public static class DynamicExtensions
    {
        public static T Cast<T>(this ExpandoObject dynamicObject) where T : class
        {
            var dymObject = (dynamic)dynamicObject;
            return (T)dymObject;
        }
    }
}
