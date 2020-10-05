using Cardinal.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cardinal.Utils.Exceptions
{
    /// <summary>
    /// Objeto que representa uma mensagem de validação da exceção <see cref="ValidationException"/>.
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Propriedade associada à falha.
        /// </summary>
        public string Field { get; }

        /// <summary>
        /// Mensagem de exceção da falha.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="field">Propriedade associada à falha.</param>
        /// <param name="message">Mensagem de exceção da falha.</param>
        public Validation(string field, string message)
        {
            this.Field = field;
            this.Message = message;
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[FIELD:{this.Field}][MESSAGE:{this.Message}]";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ValidationResponse
    {
        /// <summary>
        /// Mensagem geral da exceção.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Lista de mensagens de validação da exceção. Veja <see cref="Validation"/>
        /// </summary>
        public IEnumerable<Validation> Validations { get; set; }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[MESSAGE:{this.Message}][VALIDATIONS:{string.Join(string.Empty, this.Validations)}]";
        }
    }

    /// <summary>
    /// Classe de exceção de validação.
    /// </summary>
    public class ValidationException : CardinalException
    {
        /// <summary>
        /// Coleção de validações contidas na exceção.
        /// </summary>
        public ICollection<Validation> Validations { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public ValidationException() : base()
        {
            this.Validations = new List<Validation>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public ValidationException(string message) : base(message)
        {
            this.Validations = new List<Validation>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="validations">Mensagens de validação.</param>
        public ValidationException(params KeyValuePair<string, string>[] validations) : base()
        {
            foreach (var validation in validations)
            {
                this.Validations.Add(new Validation(validation.Key, validation.Value));
            }
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="validations">Mensagens de validação.</param>
        public ValidationException(string message, params KeyValuePair<string, string>[] validations) : base(message)
        {
            foreach (var validation in validations)
            {
                this.Validations.Add(new Validation(validation.Key, validation.Value));
            }
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
            this.Validations = new List<Validation>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="info"> The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Validations = new List<Validation>();
        }

        /// <summary>
        /// Método que adiciona uma mensagem de validação à lista.
        /// </summary>
        /// <param name="field">Campo relacionado à validação.</param>
        /// <param name="message">Mensagem de validação.</param>
        public void Add(string field, string message)
        {
            this.Validations.Add(new Validation(field, message));
        }

        /// <summary>
        /// Método que indica se há mensagens de validação na exceção.
        /// </summary>
        /// <returns>Verdadeiro caso exista alguma mensagem de validação ou falso caso contrário.</returns>
        public bool Any()
        {
            return this.Validations.Any();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ValidationResponse GetValidationResponse()
        {
            var response = new ValidationResponse() { Message = this.Message, Validations = this.Validations };
            return response;
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return string.Join(string.Empty, this.Validations);
        }
    }
}
