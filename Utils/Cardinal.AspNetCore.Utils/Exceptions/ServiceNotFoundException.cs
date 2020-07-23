using Cardinal.AspNetCore.Utils.Localization;
using System;
using System.Net;
using Cardinal.Extensions;

namespace Cardinal.Exceptions
{
    /// <summary>
    /// Classe de exceção causada por serviço não encontrado.
    /// </summary>
    public class ServiceNotFoundException : ServiceException
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public ServiceNotFoundException(string message) : base(message)
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
            this.StatusMessage = message;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="type">Tipo de classe responsável pela exceção.</param>
        public ServiceNotFoundException(Type type) : this(Resource.ERROR_SERVICE_NOT_FOUND.SetParameters("SERVICE_NAME", type.Name))
        {

        }

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
