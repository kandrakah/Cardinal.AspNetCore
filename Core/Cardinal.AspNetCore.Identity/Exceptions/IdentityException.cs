using Cardinal.Exceptions;
using System;
using System.Net;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Exceção gerada pelo serviço de identidade.
    /// </summary>
    public class IdentityException : ServiceException
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        public IdentityException() : base()
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public IdentityException(string message) : base(message)
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        protected IdentityException(HttpStatusCode statusCode, string message, Exception innerException) : base(statusCode, message, innerException)
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        public IdentityException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        /// <param name="description">Indica se deve ser adicionado o código de erro à descrição.</param>
        public IdentityException(HttpStatusCode statusCode, string description) : base(statusCode, description)
        {
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
