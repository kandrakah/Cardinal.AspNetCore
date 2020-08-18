using Cardinal.Utils;
using Cardinal.Utils.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Método para a tradução de um texto para o idioma atual.
        /// </summary>
        /// <param name="text">Objeto referenciado</param>
        /// <param name="key">Chave de referência da tradução</param>
        /// <param name="value">Valor da tradução</param>
        /// <returns>Texto traduzido</returns>
        public static string SetParameters(this string text, string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(SetParameters(Resource.ERROR_TRANSLATION_KEY_NULL_EMPTY));
            }

            if (value == null)
            {
                value = string.Empty;
            }

            var set = new TranslationSet(key, value);
            text = SetParameters(text, set);
            return text;
        }

        /// <summary>
        /// Método para a tradução de um texto para o idioma atual.
        /// </summary>
        /// <param name="value">Objeto referenciado</param>
        /// <param name="sets">Vetor de valores à serem substituídos durante a tradução</param>
        /// <returns>Texto traduzido</returns>
        public static string SetParameters(this string value, params TranslationSet[] sets)
        {
            foreach (var set in sets)
            {
                var index = value.IndexOfAny(out int length, $"$[{set.Key}]", $"{{{set.Key}}}");                
                if (index >= 0)
                {
                    var r = value.Substring(index, length);
                    value = value.Replace(r, set.Value.ToString());
                }
            }

            return value;
        }

        /// <summary>
        /// Extensão que traz o índice de um parâmetro dentro de uma string. 
        /// Vários parâmetros podem ser informados, entretanto, o primeiro encontrado será retornado.
        /// </summary>
        /// <param name="value">Objeto referênciado</param>
        /// <param name="Length">Tamanho do valor localizado</param>
        /// <param name="parameters">Variantes do parâmetro à ser buscado</param>
        /// <returns>Índice do primeiro parâmetro à ser encontrado ou -1 caso nenhum tenha sido localizado</returns>
        public static int IndexOfAny(this string value, out int Length, params string[] parameters)
        {
            foreach(var parameter in parameters)
            {
                var index = value.IndexOf(parameter);
                if(index >= 0)
                {
                    Length = parameter.Length;
                    return index;
                }
            }
            Length = 0;
            return -1;
        }

        /// <summary>
        /// Extensão para verificação de igualdade entre strings.
        /// </summary>
        /// <param name="value">Objeto referenciado</param>
        /// <param name="comparedValue">Valor String à ser comparado</param>
        /// <param name="ignoreIfNull">Indica se deve ignorar casos nulos</param>
        /// <param name="ignoreCase">Indica se deve ignorar case</param>
        /// <returns>Verdadeiro caso as strings sejam iguais e falso caso contrário</returns>
        public static bool Equals(this string value, string comparedValue, bool ignoreIfNull = true, bool ignoreCase = true)
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
        /// Extensão para verificar se uma string atende à expressão regular informada.
        /// </summary>
        /// <param name="value">Objeto referenciado</param>
        /// <param name="pattern">Expressão regular para validação do valor string</param>
        /// <returns>Verdadeiro caso a string atenda à expressão regular e Falso caso contrário</returns>
        public static bool IsMatch(this string value, string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException(Resource.ERROR_PATTERN_NULL_EMPTY);
            }

            try
            {
                return Regex.IsMatch(value, pattern);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(Resource.ERROR_PATTERN_MATH_EXCEPTION.SetParameters("ERROR", ex.Message), ex);
            }
        }

        /// <summary>
        /// Método que verifica se uma string é numérica.
        /// </summary>
        /// <param name="value">Objeto referenciado</param>
        /// <param name="lenght">Tamanho exato do número de casas núméricas</param>
        /// <returns>Verdadeiro caso a string seja numérica e falso caso contrário</returns>
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
        /// <param name="value">Objeto referenciado</param>
        /// <param name="exclusions">Exclusões de capitalização</param>
        /// <returns>String capitalizada</returns>
        public static string Capitalize(this string value, params string[] exclusions)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var localValue = value.ToLower();
            var words = new Queue<string>();
            foreach (var word in localValue.Split(' '))
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
        /// Extensão para converter uma string em um vetor de bytes.
        /// </summary>
        /// <param name="value">Objeto referenciado</param>
        /// <returns>Vetor de bytes gerado a partir da string</returns>
        public static byte[] ToByteArray(this string value)
        {
            return Encoding.Default.GetBytes(value);
        }

        /// <summary>
        /// Extensão para converter uma string em um vetor de bytes. 
        /// </summary>
        /// <param name="value">Objeto referenciado.</param>
        /// <param name="encoding">Encoder à ser utilizado na conversão.</param>
        /// <returns>Vetor de bytes gerado a partir da string.</returns>
        public static byte[] ToByteArray(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// Extensão que computa um hash de um string.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="hashAlgoritm">Algoritmo utilizado no cálculo de hash. Veja <see cref="HashAlgorithmName"/></param>
        /// <returns>Hash gerado para o string informado</returns>
        public static byte[] ComputeHash(this string source, HashAlgorithmName hashAlgoritm)
        {
            var valueBytes = source.ToByteArray();
            var hash = valueBytes.ComputeHash(hashAlgoritm);
            return hash;
        }

        /// <summary>
        /// Extensão que computa um hash de um string.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="hashAlgoritm">Algoritmo utilizado no cálculo de hash. Veja <see cref="HashAlgorithmName"/></param>
        /// <param name="encoding">Codificação da string</param>
        /// <returns>Hash gerado para o string informado</returns>
        public static byte[] ComputeHash(this string source, HashAlgorithmName hashAlgoritm, Encoding encoding)
        {
            var valueBytes = source.ToByteArray(encoding);
            var hash = valueBytes.ComputeHash(hashAlgoritm);
            return hash;
        }
    }
}
