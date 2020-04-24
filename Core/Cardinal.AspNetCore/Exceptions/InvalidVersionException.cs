using System;

namespace Cardinal.AspNetCore.Exceptions
{
    /// <summary>
    /// Classe de exceção ligada à validação de versões.
    /// </summary>
    public class InvalidVersionException : CardinalException
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public InvalidVersionException(string message) : base(message) { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        public InvalidVersionException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return this.Message;
        }
    }
}
