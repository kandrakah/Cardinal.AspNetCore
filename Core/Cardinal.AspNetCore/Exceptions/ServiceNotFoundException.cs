using Cardinal.AspNetCore.Localization;
using Cardinal.AspNetCore.Utils;
using System;
using System.Net;

namespace Cardinal.AspNetCore.Exceptions
{
    /// <summary>
    /// Classe de excessão causada por serviço não encontrado.
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
        public ServiceNotFoundException(Type type) : this(ResourceUtils.Translate(Resource.ERROR_SERVICE_NOT_FOUND, ResourceUtils.Set("SERVICE_NAME", type.Name)))
        {

        }
    }
}
