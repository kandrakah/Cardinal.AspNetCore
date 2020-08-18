using Cardinal.AspNetCore.Interfaces;
using Cardinal.Exceptions;
using Cardinal.Extensions;
using Cardinal.Utils;
using Cardinal.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

namespace Cardinal.AspNetCore.Controllers
{
    /// <summary>
    /// Classe base para todos os controllers do sistema.
    /// </summary>
    public abstract class DefaultController : ControllerBase, IController
    {
        /// <summary>
        /// Instância do serviço de logs.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Instância do provedor de serviços. Veja <see cref="IServiceProvider"/> para mais detalhes.
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="loggerFactory">Instância do serviço de logs.</param>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public DefaultController(ILoggerFactory loggerFactory, IServiceProvider provider)
        {
            this.Logger = loggerFactory.CreateLogger(this.GetType().Name);
            this.ServiceProvider = provider;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="logger">Instância do serviço de logs.</param>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public DefaultController(ILogger logger, IServiceProvider provider)
        {
            this.Logger = logger;
            this.ServiceProvider = provider;
        }

        /// <summary>
        /// Método que obtém a instância de um serviço.
        /// </summary>
        /// <typeparam name="T">Interface do serviço solicitada.</typeparam>
        /// <returns>Instância do serviço solicitado.</returns>
        protected T GetService<T>()
        {
            var result = this.ServiceProvider.GetCardinalService<T>();
            return result;
        }

        /// <summary>
        /// Método que obtém a instância de um serviço.
        /// </summary>
        /// <param name="service">Tipo do serviço solicitado.</param>
        /// <returns>Instância do serviço solicitado.</returns>
        protected object GetService(Type service)
        {
            var result = this.ServiceProvider.GetCardinalService(service);
            return result;
        }

        /// <summary>
        /// Método que obtém um item do header da requisição.
        /// </summary>
        /// <param name="key">Identificador do header requerido</param>
        /// <returns>Valor do header requerido</returns>
        protected string GetRequestHeader(string key)
        {
            var result = this.Request.Headers.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected StatusCodeResult StatusCode(HttpStatusCode statusCode)
        {
            return this.StatusCode((int)statusCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected ObjectResult StatusCode(HttpStatusCode statusCode, object obj)
        {
            return this.StatusCode((int)statusCode, obj);
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="exception">Excessão à ser respondida à requisição.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(Exception exception)
        {
            if (exception is ServiceException)
            {
                var ex = (ServiceException)exception;
                this.Logger.LogError(ex, ex.StatusMessage);
                return this.Error((int)ex.StatusCode, ex.StatusMessage);
            }
            else if (exception is ValidationException)
            {
                var ex = exception as ValidationException;
                this.Logger.LogError(ex, ex.Message);
                return this.Error((int)HttpStatusCode.BadRequest, ex, ex.GetValidationResponse());

            }
            else
            {
                this.Logger.LogError(exception, exception.Message);
                return this.Error((int)HttpStatusCode.InternalServerError, exception.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da excessão.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(HttpStatusCode statusCode)
        {
            return this.Error((int)statusCode, statusCode.GetDescription());
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da excessão.</param>
        /// <param name="message">Mensagem à ser apresentada na excessão.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(int statusCode, string message)
        {
            this.Logger.LogError($"{statusCode}:{message}");
            return this.StatusCode(statusCode, message);
        }

        /// <summary>
        /// Método que responte à requisição com um erro.
        /// </summary>
        /// <param name="statusCode">Código de erro da excessão.</param>
        /// <param name="exception">Excessão à ser respondida à requisição.</param>
        /// <param name="obj">Objeto à ser apresentado na excessão.</param>
        /// <returns>Resposta à ser devolvida ao requerente da requisição. <see cref="ActionResult"/></returns>
        protected ActionResult Error(int statusCode, Exception exception, object obj)
        {
            this.Logger.LogError($"{statusCode}:{StatusCodeUtils.GetDescription(statusCode)}", exception);
            return this.StatusCode(statusCode, obj);
        }
    }
}
