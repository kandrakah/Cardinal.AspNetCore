using Cardinal.AspNetCore.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IController"/>.
    /// </summary>
    public static class ICardinalControllerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static string GetDefaultPermission(this IController controller)
        {
            var controllerName = controller.GetType().Name;
            var tag = Regex.Replace(controllerName, "CONTROLLER", string.Empty, RegexOptions.IgnoreCase).Trim().ToUpper(CultureInfo.InvariantCulture);
            return tag.ToUpper();
        }
    }
}
