using System;
using System.Globalization;

namespace Cardinal.AspNetCore.Identity.Extensions
{
    public static class TypeExtensions
    {
        public static string GetControllerDefaultPermission(this Type type)
        {
            var tag = type.Name.ToUpper(CultureInfo.InvariantCulture).Replace("CONTROLLER", string.Empty, StringComparison.InvariantCulture).Trim().ToUpper(CultureInfo.InvariantCulture);
            return tag.ToUpper();
        }
    }
}
