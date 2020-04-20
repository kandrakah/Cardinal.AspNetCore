using Cardinal.AspNetCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace Cardinal.AspNetCore.Identity.Extensions
{
    public static class ICardinalControllerExtensions
    {
        public static string GetDefaultPermission(this ICardinalController controller)
        {
            var controllerName = controller.GetType().Name;
            var tag = controllerName.ToUpper(CultureInfo.InvariantCulture).Replace("CONTROLLER", string.Empty, StringComparison.InvariantCulture).Trim().ToUpper(CultureInfo.InvariantCulture);
            return tag.ToUpper();
        }
    }
}
