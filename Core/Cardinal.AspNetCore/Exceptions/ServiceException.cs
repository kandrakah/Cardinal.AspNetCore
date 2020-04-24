using Cardinal.AspNetCore.Extensions;
using System;
using System.Net;

namespace Cardinal.AspNetCore.Exceptions
{
    /// <summary>
    /// Classe de exceção de serviços de Api.
    /// </summary>
    public class ServiceException : CardinalException
    {
        /// <summary>
        /// Código de status da exceção.
        /// </summary>
        public HttpStatusCode StatusCode { get; protected set; }

        /// <summary>
        /// Mensagem de status da exceção.
        /// </summary>
        public string StatusMessage { get; protected set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public ServiceException() : base()
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
            this.StatusMessage = this.StatusCode.GetDescription();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public ServiceException(string message) : base(message)
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
            this.StatusMessage = message;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        protected ServiceException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            this.StatusCode = statusCode;
            this.StatusMessage = message;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        /// <param name="includeStatusCode">Indica se deve ser adicionado o código de erro à descrição.</param>
        public ServiceException(HttpStatusCode statusCode, bool includeStatusCode = false) : this(statusCode, statusCode.GetDescription(includeStatusCode))
        {
            this.StatusCode = statusCode;
            this.StatusMessage = statusCode.GetDescription(includeStatusCode);
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="statusCode">Código de status da exceção.</param>
        /// <param name="description">Indica se deve ser adicionado o código de erro à descrição.</param>
        public ServiceException(HttpStatusCode statusCode, string description) : base(description)
        {
            this.StatusCode = statusCode;
            this.StatusMessage = description;
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[CODE:{(int)this.StatusCode}][MESSAGE:{this.StatusMessage}]";
        }
    }
}
