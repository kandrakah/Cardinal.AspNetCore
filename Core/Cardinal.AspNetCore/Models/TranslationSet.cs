namespace Cardinal.AspNetCore.Models
{
    /// <summary>
    /// Classe de definição de valor de tradução.
    /// </summary>
    public class TranslationSet
    {
        /// <summary>
        /// Chave da tradução.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Valor da tradução.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public TranslationSet()
        {

        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="key">Chave da tradução.</param>
        /// <param name="value">Valor da tradução.</param>
        public TranslationSet(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Método estático para definição de valor de tradução.
        /// </summary>
        /// <param name="key">Chave da tradução.</param>
        /// <param name="value">Valor da tradução.</param>
        /// <returns>Instância de <see cref="TranslationSet"/>.</returns>
        public static TranslationSet Set(string key, object value)
        {
            return new TranslationSet(key, value);
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[KEY:{this.Key}][VALUE:{this.Value}]";
        }
    }
}
