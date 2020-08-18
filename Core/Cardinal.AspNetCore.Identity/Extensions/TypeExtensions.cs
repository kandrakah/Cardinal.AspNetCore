using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetControllerDefaultPermission(this Type type)
        {
            var tag = Regex.Replace(type.Name, "CONTROLLER", string.Empty, RegexOptions.IgnoreCase).Trim().ToUpper(CultureInfo.InvariantCulture);
            return tag.ToUpper();
        }
    }
}
