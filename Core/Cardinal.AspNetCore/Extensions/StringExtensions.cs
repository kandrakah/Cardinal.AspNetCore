using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cardinal.AspNetCore.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparedValue"></param>
        /// <param name="ignoreIfNull"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool IsEquals(this string value, string comparedValue, bool ignoreIfNull = true, bool ignoreCase = true)
        {
            if (ignoreIfNull && (value == null || comparedValue == null))
            {
                return value == comparedValue;
            }
            else if (ignoreCase)
            {
                return value.Trim().ToUpper().Equals(comparedValue.Trim().ToUpper());
            }
            else
            {
                return value.Trim().Equals(comparedValue.Trim());
            }
        }

        /// <summary>
        /// Método que verifica se uma string é numérica.
        /// </summary>
        /// <param name="value">Valor string à ser validado.</param>
        /// <param name="lenght">Tamanho exato do número de casas núméricas.</param>
        /// <returns></returns>
        public static bool IsNumeric(this string value, int lenght = -1)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (lenght >= 0)
            {
                return Regex.IsMatch(value, @"^\d+$") && value.Length == lenght;
            }
            else
            {
                return Regex.IsMatch(value, @"^\d+$");
            }
        }
        
        /// <summary>
        /// Método que capitaliza uma string.
        /// </summary>
        /// <param name="value">Valor à ser capitalizado.</param>
        /// <param name="exclusions">Exclusões de capitalização.</param>
        /// <returns>String capitalizada.</returns>
        public static string Capitalize(this string value, params string[] exclusions)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var localValue = value.ToLower();
            var words = new Queue<string>();
            foreach(var word in localValue.Split(' '))
            {
                if (!string.IsNullOrEmpty(word))
                {
                    var lower = word.ToLower();
                    var letters = lower.ToCharArray();
                    if (!exclusions.Contains(lower))
                    {
                        letters[0] = char.ToUpper(letters[0]);
                    }
                    words.Enqueue(new string(letters));
                }
            }

            return string.Join(" ", words);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string value)
        {
            return Encoding.Default.GetBytes(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }
    }
}
