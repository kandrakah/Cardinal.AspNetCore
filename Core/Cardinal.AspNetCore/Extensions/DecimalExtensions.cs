using Cardinal.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Cardinal.AspNetCore.Extensions
{
    public static class DecimalExtensions
    {
        public static string AsCurrency(this decimal value)
        {
            return string.Format(Resource.Culture, "{0:C}", value);
        }

        public static string AsCurrency(this decimal? value)
        {
            return value != null ? string.Format(Resource.Culture, "{0:C}", value) : string.Format(Resource.Culture, "{0:C}", 0);
        }

        public static string AsCurrency(this decimal value, CultureInfo culture)
        {
            return string.Format(culture, "{0:C}", value);
        }

        public static string AsCurrency(this decimal? value, CultureInfo culture)
        {
            return value != null ? string.Format(culture, "{0:C}", value) : string.Format(culture, "{0:C}", 0);
        }

        public static string AsPercentage(this decimal value, bool transform = false)
        {
            return transform ? $"{value * 100}%" : $"{value}%";
        }

        public static string AsPercentage(this decimal? value, bool transform = false)
        {
            return value != null ? transform ? $"{value * 100}%" : $"{value}%" : "0%";
        }
    }
}
