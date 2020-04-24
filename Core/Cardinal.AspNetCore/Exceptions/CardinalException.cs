using System;
using System.Runtime.Serialization;

namespace Cardinal.AspNetCore.Exceptions
{
    /// <summary>
    /// Classe de exceção de serviços de Api.
    /// </summary>
    public class CardinalException : Exception
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public CardinalException() : base() { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public CardinalException(string message) : base(message) { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        public CardinalException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="info"> The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public CardinalException(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
