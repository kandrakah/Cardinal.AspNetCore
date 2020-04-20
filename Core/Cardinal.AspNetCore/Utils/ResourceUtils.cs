using Cardinal.AspNetCore.Models;
using System.Collections.Generic;

namespace Cardinal.AspNetCore.Utils
{
    /// <summary>
    /// Classe estática de utilidades para recursos.
    /// </summary>
    public static class ResourceUtils
    {        
        /// <summary>
        /// Método para a tradução de um texto para o idioma atual.
        /// </summary>
        /// <param name="text">Texto à ser traduzido.</param>
        /// <param name="values">Vetor de valores à serem substituídos durante a tradução.</param>
        /// <returns>Texto traduzido.</returns>
        public static string Translate(string text, params TranslationSet[] sets)
        {
            foreach (var set in sets)
            {
                var key = $"$[{set.Key}]";
                var index = text.IndexOf(key);
                if (index > 0)
                {
                    var r = text.Substring(index, key.Length);
                    text = text.Replace(r, set.Value.ToString());
                }
            }

            return text;
        }

        /// <summary>
        /// Método que cria um parâmetro de tradução.
        /// </summary>
        /// <param name="key">Chave to elemento.</param>
        /// <param name="value">Valor do elemento.</param>
        /// <returns>Parâmetro de tradução do texto.</returns>
        public static TranslationSet Set(string key, object value)
        {
            return TranslationSet.Set(key, value);
        }
    }
}
